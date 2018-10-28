using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public struct LightPair {
    public Light light;
    public float maxIntensity;
}

public class LightningFlashChild : MonoBehaviour
{

    public float minTime = 0.2f;
    public float threshold = 0.7f;
    public float damping = 40f;
    List<LightPair> myLights;

    public float start = 3f;
    public float end = 4.5f;

    float intensityMax;

    float lastTime = 0f;

    public int seed = 9;

    float intensity = 0f;
    float intensityVelocity = 0f;

    // Use this for initialization
    void Start()
    {
        Random.InitState(seed);

        myLights = new List<LightPair>();

        foreach (GameObject g in getChildren()) {
            Light l = (Light)g.GetComponent("Light");
            if (l != null) {
                ReadMe m = (ReadMe)g.GetComponent("ReadMe");
                if (m.enabled) {
                    LightPair create;
                    create.light = l;
                    create.maxIntensity = l.intensity;
                    myLights.Add(create);
                    l.enabled = true;
                }
            }
        }
    }

    GameObject[] getChildren() {
        int i = 0;

        //Array to hold all child obj
        GameObject[] allChildren = new GameObject[transform.childCount];

        //Find all child obj and store to that array
        foreach (Transform child in transform)
        {
            allChildren[i] = child.gameObject;
            i += 1;
        }
        return allChildren;
    }

    // Update is called once per frame
    void Update()
    {
        intensity += intensityVelocity * Time.deltaTime;
        intensityVelocity += (-damping * intensity * intensity) * Time.deltaTime;
        if (intensity < 0) {
            intensityVelocity = 0;
            intensity = 0;
        }
        if (intensity > 1) {
            intensityVelocity = 0;
            intensity = 1;
        }


        foreach (LightPair p in myLights) {
            p.light.intensity = p.maxIntensity * intensity;
        }
        if ((Time.time - lastTime) > minTime && intensityVelocity <= 0 && intensity < 0.4 && Time.time > start && Time.time < end)
        {
            if (Random.value > threshold)
            {
                intensityVelocity = 2 * damping;
                Debug.Log("LIGHT");
            }
            else
            {
                lastTime = Time.time;
            }
        }
    }
}

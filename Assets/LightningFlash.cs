using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningFlash : MonoBehaviour
{

    public float minTime = 0.2f;
    public float threshold = 0.7f;
    Light myLight;
    public float damping = 40f;

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
        myLight = (Light)this.GetComponent("Light");
        intensityMax = myLight.intensity;
        myLight.enabled = true;
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



        myLight.intensity = intensityMax * intensity;
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

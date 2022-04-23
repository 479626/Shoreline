using System;
using UnityEngine.Rendering;
using UnityEngine;

public class Light : MonoBehaviour
{
    public Volume volume;
    public int hour;

    void Start()
    {
        volume = gameObject.GetComponent<Volume>();
    }

    void FixedUpdate()
    {
        UpdateLight();
    }

    void UpdateLight()
    {
        // Getting the current hour from the user's system time
        DateTime time = DateTime.Now;
        hour = time.Hour;
        
        // Light changing logic
        if (hour >= 15 && hour < 18) // 3pm - 6pm
        {
            volume.weight = 0.25f;
        }
        if (hour >= 19 && hour < 20) // 7pm - 8pm
        {
            volume.weight = 0.5f;
        }
        if (hour >= 21 && hour < 22) // 9pm - 10pm
        {
            volume.weight = 0.75f;
        }
        if (hour >= 22 && hour < 23) // 10pm - 11pm
        {
            volume.weight = 0.8f;
        }
        if (hour == 23) // 11pm
        {
            volume.weight = 0.9f;
        }
        if (hour >= 0 && hour < 2) // 12am - 2am
        {
            volume.weight = 1f;
        }
        if (hour == 3) // 3am
        {
            volume.weight = 0.9f;
        }
        if (hour == 4) // 4am
        {
            volume.weight = 0.8f;
        }
        if (hour == 5) // 5am
        {
            volume.weight = 0.7f;
        }
        if (hour == 6) // 6am
        {
            volume.weight = 0.5f;
        }
        if (hour == 7) // 7am
        {
            volume.weight = 0.25f;
        }
        if (hour >= 8 && hour < 14) // 8am - 2pm
        {
            volume.weight = 0f;
        }
    }
}

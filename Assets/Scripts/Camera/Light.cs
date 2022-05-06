using System;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Light : MonoBehaviour
{
    public Volume volume;
    public int hour;

    private void Start()
    {
        volume = gameObject.GetComponent<Volume>();
    }

    private void FixedUpdate()
    {
        if (SceneManager.GetActiveScene().name == "Battle-Cutscene" || SceneManager.GetActiveScene().name == "L2-Battle") return;
        UpdateLight();
    }

    private void UpdateLight()
    {
        var time = DateTime.Now;
        hour = time.Hour;

        if (hour >= 15 && hour < 18)
        {
            volume.weight = 0.25f;
        }
        if (hour >= 19 && hour < 20)
        {
            volume.weight = 0.5f;
        }
        if (hour >= 21 && hour < 22)
        {
            volume.weight = 0.75f;
        }
        if (hour >= 22 && hour < 23)
        {
            volume.weight = 0.8f;
        }
        if (hour == 23)
        {
            volume.weight = 0.9f;
        }
        if (hour >= 0 && hour < 2)
        {
            volume.weight = 1f;
        }
        if (hour == 3)
        {
            volume.weight = 0.9f;
        }
        if (hour == 4)
        {
            volume.weight = 0.8f;
        }
        if (hour == 5)
        {
            volume.weight = 0.7f;
        }
        if (hour == 6)
        {
            volume.weight = 0.5f;
        }
        if (hour == 7)
        {
            volume.weight = 0.25f;
        }
        if (hour >= 8 && hour < 14)
        {
            volume.weight = 0f;
        }
    }
}

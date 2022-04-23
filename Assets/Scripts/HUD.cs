using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public static HUD instance;
    public GameObject display;
    public InteractionCounter counter;
    [SerializeField] private Text currentTime, coinCount;

    void Awake()
    {
        if (SceneManager.GetActiveScene().name != "User-Interface")
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    void Update()
    {
        UpdateDisplay();
    }

    void UpdateDisplay()
    {
        currentTime.text = DateTime.Now.ToString("hh:mm tt");
        coinCount.text = counter.coins.ToString();
    }

    public IEnumerator LoadDisplay()
    {
        yield return new WaitForSeconds(1f);
        display.SetActive(true);
        Debug.Log("Turned on display");
    }
}

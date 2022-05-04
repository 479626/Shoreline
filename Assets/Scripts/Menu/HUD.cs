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
    public PlayerStats stats;
    private bool exist;
    [SerializeField] private Text currentTime, coinCount;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            exist = true;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        UpdateDisplay();

        if (exist && SceneManager.GetActiveScene().name == "User-Interface")
        {
            exist = false;
            Destroy(gameObject);
        }
    }

    private void UpdateDisplay()
    {
        currentTime.text = DateTime.Now.ToString("hh:mm tt");
        coinCount.text = stats.coins.ToString();
    }

    public IEnumerator LoadDisplay()
    {
        yield return new WaitForSeconds(1f);
        display.SetActive(true);
    }
}

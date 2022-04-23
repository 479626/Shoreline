using System;
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
        display.SetActive(false);

        if (SceneManager.GetActiveScene().buildIndex != 1)
        {
            Debug.Log("Enabled");
            display.SetActive(true);
        }

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

    void Update()
    {
        UpdateDisplay();
    }

    void UpdateDisplay()
    {
        currentTime.text = DateTime.Now.ToString("hh:mm tt");
        coinCount.text = counter.coins.ToString();
    }
}

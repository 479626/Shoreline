using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public static HUD instance;
    public GameObject display;
    public InteractionCounter counter;
    [SerializeField] private Text currentTime;
    [SerializeField] private Text coinCount;

    void Awake()
    {
        display.SetActive(false);
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
        if (SceneManager.GetActiveScene().buildIndex > 1)
        {
            display.SetActive(true);
        }

        UpdateDisplay();
    }

    void UpdateDisplay()
    {
        currentTime.text = DateTime.Now.ToString("hh:mm tt");
        coinCount.text = counter.coins.ToString();
    }
}

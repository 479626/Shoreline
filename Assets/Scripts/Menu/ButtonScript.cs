using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using System.Collections.Generic;
using System.Collections;

public class ButtonScript : MonoBehaviour
{
    [SerializeField] private List<Button> startButtons = new List<Button>();
    [SerializeField] private List<Text> settingsButtonText = new List<Text>();
    [SerializeField] private List<GameObject> menuObjects = new List<GameObject>();
    [SerializeField] private InteractionCounter count;
    [SerializeField] private PlayerStats stats;
    [SerializeField] private AudioMixer mixer;

    private bool vsync, fullscreen, muted;
    public float transitionTime = 1f;

    private void Start()
    {
        startButtons[0].Select();
        StartCoroutine(CheckPlayerPrefs());
    }

    public IEnumerator CheckPlayerPrefs()
    {
        var waitTime = new WaitForSeconds(.1f);

        yield return waitTime;
        PlayerPrefs.GetInt("fullscreen", 1);
        PlayerPrefs.GetInt("vsync", 0);
        PlayerPrefs.DeleteKey("muted");
        yield return waitTime;
        PlayerPrefs.GetInt("mute", 0);
        CheckSettings();
        yield return null;
    }

    public void CheckSettings()
    {
        if (PlayerPrefs.GetInt("vsync") == 0)
        {
            vsync = false;
            settingsButtonText[0].color = Color.red;
            settingsButtonText[0].text = "Off";
        }
        else
        {
            vsync = true;
            settingsButtonText[0].color = Color.green;
            settingsButtonText[0].text = "On";
        }

        if (PlayerPrefs.GetInt("mute") == 0)
        {
            muted = false;
            settingsButtonText[2].color = Color.red;
            settingsButtonText[2].text = "Off";
        }
        else
        {
            muted = true;
            settingsButtonText[2].color = Color.green;
            settingsButtonText[2].text = "On";
        }

        if (PlayerPrefs.GetInt("fullscreen") == 0)
        {
            fullscreen = false;
            settingsButtonText[1].color = Color.red;
            settingsButtonText[1].text = "Off";
        }
        else
        {
            fullscreen = true;
            settingsButtonText[1].color = Color.green;
            settingsButtonText[1].text = "On";
        }
    }

    #region Button Methods

    public void OnStartButton()
    {
        Time.timeScale = 1f;
        SoundManager.instance.PurchaseSound();
    }

    public void OnQuitButton()
    {
        Application.Quit();
    }

    public void OnControlsButton()
    {
        menuObjects[2].SetActive(false);
        menuObjects[4].SetActive(true);
        startButtons[4].Select();
    }
    public void OnControlsExit()
    {
        menuObjects[4].SetActive(false);
        menuObjects[2].SetActive(true);
        startButtons[2].Select();
    }

    public void OnSettingsButton()
    {
        menuObjects[0].SetActive(false);
        menuObjects[1].SetActive(true);
        startButtons[1].Select();
    }

    public void OnSettingsExit()
    {
        menuObjects[1].SetActive(false);
        menuObjects[0].SetActive(true);
        startButtons[0].Select();
    }

    public void OnOptionsButton()
    {
        StartCoroutine(CheckPlayerPrefs());
        menuObjects[1].SetActive(false);
        menuObjects[2].SetActive(true);
        startButtons[2].Select();
    }

    public void OnOptionsExit()
    {
        menuObjects[2].SetActive(false);
        menuObjects[1].SetActive(true);
        startButtons[1].Select();
    }

    public void OnSoundsButton()
    {
        menuObjects[1].SetActive(false);
        menuObjects[3].SetActive(true);
        startButtons[3].Select();
    }

    public void OnSoundsExit()
    {
        menuObjects[3].SetActive(false);
        menuObjects[1].SetActive(true);
        startButtons[1].Select();
    }

    public void OnVsyncButton()
    {
        if (!vsync)
        {
            settingsButtonText[0].color = Color.green;
            settingsButtonText[0].text = "On";
            vsync = true;
            QualitySettings.vSyncCount = 1;
            PlayerPrefs.SetInt("vsync", 1);
            Debug.Log(QualitySettings.vSyncCount);
        }
        else
        {
            settingsButtonText[0].color = Color.red;
            settingsButtonText[0].text = "Off";
            vsync = false;
            QualitySettings.vSyncCount = 0;
            PlayerPrefs.SetInt("vsync", 0);
            Debug.Log(QualitySettings.vSyncCount);
        }
    }

    public void OnFullscreenButton()
    {
        if (!fullscreen)
        {
            settingsButtonText[1].color = Color.green;
            settingsButtonText[1].text = "On";
            fullscreen = true;
            Screen.fullScreen = true;
            PlayerPrefs.SetInt("fullscreen", 1);
            Debug.Log(Screen.fullScreen);
        }
        else
        {
            settingsButtonText[1].color = Color.red;
            settingsButtonText[1].text = "Off";
            fullscreen = false;
            Screen.fullScreen = false;
            PlayerPrefs.SetInt("fullscreen", 0);
            Debug.Log(Screen.fullScreen);
        }
    }

    public void OnMuteButton()
    {
        Debug.Log("Mute pressed [" + muted + "]");
        if (muted)
        {
            settingsButtonText[2].color = Color.red;
            settingsButtonText[2].text = "Off";
            muted = false;
            mixer.SetFloat(Sliders.MASTER, 1f);
            PlayerPrefs.SetInt("muted", 0);
        }
        else
        {
            settingsButtonText[2].color = Color.green;
            settingsButtonText[2].text = "On";
            muted = true;
            mixer.SetFloat(Sliders.MASTER, -80f);
            PlayerPrefs.SetInt("muted", 1);
        }
        Debug.Log("Mute set [" + muted + "]");
    }

    private void OnDisable()
    {
        if (muted)
        {
            PlayerPrefs.SetInt("mute", 1);
        }
        else
        {
            PlayerPrefs.SetInt("mute", 0);
        }

        if (fullscreen)
        {
            PlayerPrefs.SetInt("fullscreen", 1);
        }
        else
        {
            PlayerPrefs.SetInt("fullscreen", 0);
        }

        if (vsync)
        {
            PlayerPrefs.SetInt("vsync", 1);
        }
        else
        {
            PlayerPrefs.SetInt("vsync", 0);
        }
    }

    public void PlaySound()
    {
        SoundManager.instance.PurchaseSound();
    }

    #endregion
}

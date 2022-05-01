using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using System.Collections.Generic;
using System.Collections;

public class ButtonScript : MonoBehaviour
{
    public bool vsync, fullscreen, muted;
    public List<Button> startButtons = new List<Button>();
    public List<Text> settingsButtonText = new List<Text>();
    public List<GameObject> menuObjects = new List<GameObject>();
    public InteractionCounter count;
    public PlayerStats stats;
    [SerializeField] AudioMixer mixer;

    public float transitionTime = 1f;

    void Start()
    {
        startButtons[0].Select();
        StartCoroutine(CheckPlayerPrefs());
    }

    IEnumerator CheckPlayerPrefs()
    {
        yield return new WaitForSeconds(.1f);
        PlayerPrefs.GetInt("fullscreen", 1);
        PlayerPrefs.GetInt("vsync", 0);
        PlayerPrefs.DeleteKey("muted");
        yield return new WaitForSeconds(.1f);
        PlayerPrefs.GetInt("mute", 0);
        CheckSettings();
        yield break;
    }

    public void CheckSettings()
    {
        // Check for current vsync
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

        // Check for muted audio
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

        // Check for current fullscreen
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
            float musicVolume = PlayerPrefs.GetFloat(SoundManager.MUSIC_KEY, 0.5f);
            float sfxVolume = PlayerPrefs.GetFloat(SoundManager.SFX_KEY, 0.5f);

            mixer.SetFloat(Sliders.MUSIC, Mathf.Log10(musicVolume) * 20);
            mixer.SetFloat(Sliders.SFX, Mathf.Log10(sfxVolume) * 20);
            PlayerPrefs.SetInt("muted", 0);
        }
        else
        {
            settingsButtonText[2].color = Color.green;
            settingsButtonText[2].text = "On";
            muted = true;
            mixer.SetFloat(Sliders.MUSIC, -80f);
            mixer.SetFloat(Sliders.SFX, -80f);
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

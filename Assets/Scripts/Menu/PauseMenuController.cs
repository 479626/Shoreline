using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class PauseMenuController : MonoBehaviour
{
    public static PauseMenuController instance;

    [SerializeField] private List<Button> startButtons = new List<Button>();
    [SerializeField] private List<Text> settingsButtonText = new List<Text>();
    [SerializeField] private List<GameObject> menuObjects = new List<GameObject>();
    [SerializeField] private AudioMixer mixer;

    private bool vsync, fullscreen, muted;
    private bool settingsMenuOpen, soundsMenuOpen, optionsMenuOpen, mainMenuOpen, controlsMenuOpen;
    public static bool gamePaused;

    private void Awake()
    {
        menuObjects[0].SetActive(false);
        gamePaused = false; 
        Time.timeScale = 1f;

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

    private void Update()
    {
        CheckForPause();
    }

    public void PlaySound()
    {
        SoundManager.instance.PurchaseSound();
    }

    private void CheckForPause()
    {
        if (SceneManager.GetActiveScene().name != "User-Interface" && Input.GetKeyDown(KeyCode.Escape))
        {
            if (settingsMenuOpen)
            {
                if (Input.GetKey(KeyCode.Escape))
                {
                    OnSettingsExit();
                }
            }

            if (optionsMenuOpen)
            {
                if (Input.GetKey(KeyCode.Escape))
                {
                    OnOptionsExit();
                }
            }

            if (controlsMenuOpen)
            {
                if (Input.GetKey(KeyCode.Escape))
                {
                    OnControlsExit();
                }
            }

            if (soundsMenuOpen)
            {
                if (Input.GetKey(KeyCode.Escape))
                {
                    OnSoundsExit();
                }
            }

            if (gamePaused && mainMenuOpen)
            {
                mainMenuOpen = false;
                Resume();
            }
            else if (!settingsMenuOpen && !mainMenuOpen && !gamePaused && Time.timeScale != 0)
            {
                mainMenuOpen = true;
                OpenPauseMenu();
            }
        }
    }

    private void OpenPauseMenu()
    {
        menuObjects[0].SetActive(true);
        StartCoroutine(CheckPlayerPrefs());
        Time.timeScale = 0f;
        gamePaused = true;
    }

    #region Button Methods

    public void OnControlsButton()
    {
        controlsMenuOpen = true;
        menuObjects[2].SetActive(false);
        menuObjects[4].SetActive(true);
        startButtons[4].Select();
    }
    public void OnControlsExit()
    {
        controlsMenuOpen = false;
        menuObjects[4].SetActive(false);
        menuObjects[2].SetActive(true);
        startButtons[2].Select();
    }

    public void OnSettingsButton()
    {
        settingsMenuOpen = true;
        menuObjects[0].SetActive(false);
        menuObjects[1].SetActive(true);
        startButtons[1].Select();
    }

    public void OnSettingsExit()
    {
        settingsMenuOpen = false;
        menuObjects[1].SetActive(false);
        menuObjects[0].SetActive(true);
        startButtons[0].Select();
    }

    public void OnOptionsButton()
    {
        optionsMenuOpen = true;
        settingsMenuOpen = false;
        menuObjects[1].SetActive(false);
        menuObjects[2].SetActive(true);
        startButtons[2].Select();
    }

    public void OnOptionsExit()
    {
        optionsMenuOpen = false;
        settingsMenuOpen = false;
        menuObjects[2].SetActive(false);
        menuObjects[1].SetActive(true);
        startButtons[1].Select();
    }

    public void OnSoundsButton()
    {
        soundsMenuOpen = true;
        settingsMenuOpen = false;
        menuObjects[1].SetActive(false);
        menuObjects[3].SetActive(true);
        startButtons[3].Select();
    }

    public void OnSoundsExit()
    {
        soundsMenuOpen = false;
        settingsMenuOpen = false;
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
        }
        else
        {
            settingsButtonText[0].color = Color.red;
            settingsButtonText[0].text = "Off";
            vsync = false;
            QualitySettings.vSyncCount = 0;
            PlayerPrefs.SetInt("vsync", 0);
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
        }
        else
        {
            settingsButtonText[1].color = Color.red;
            settingsButtonText[1].text = "Off";
            fullscreen = false;
            Screen.fullScreen = false;
            PlayerPrefs.SetInt("fullscreen", 0);
        }
    }

    public void OnMuteButton()
    {
        if (muted)
        {
            settingsButtonText[2].color = Color.red;
            settingsButtonText[2].text = "Off";
            muted = false;
            mixer.SetFloat(Sliders.MASTER, 1f);
            PlayerPrefs.SetInt("muted", 0);
            StartCoroutine(CheckPlayerPrefs());
        }
        else
        {
            settingsButtonText[2].color = Color.green;
            settingsButtonText[2].text = "On";
            muted = true;
            mixer.SetFloat(Sliders.MASTER, -80f);
            PlayerPrefs.SetInt("muted", 1);
            StartCoroutine(CheckPlayerPrefs());
        }
    }

    public void Quit()
    {
        Resume();
        SceneManager.LoadScene(1);
    }

    public void Resume()
    {
        if (gamePaused)
        {
            menuObjects[0].SetActive(false);
            menuObjects[1].SetActive(false);
            menuObjects[2].SetActive(false);
            menuObjects[3].SetActive(false);
            menuObjects[4].SetActive(false);
            Time.timeScale = 1f;
            gamePaused = false;
            settingsMenuOpen = false;
            optionsMenuOpen = false;
            soundsMenuOpen = false;
        }
    }

    #endregion
}

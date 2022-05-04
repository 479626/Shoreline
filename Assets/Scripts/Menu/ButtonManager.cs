using UnityEngine.SceneManagement;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    public static ButtonManager instance;
    public static bool gamePaused;
    private bool settingsMenuOpen;
    public GameObject pauseMenu, settingsMenu;

    private void Awake()
    {
        pauseMenu.SetActive(false);
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
                    OnSettingsClose();
                }
            }

            if (gamePaused)
            {
                Resume();
            }
            else if (!settingsMenuOpen && !gamePaused && Time.timeScale != 0)
            {
                OpenPauseMenu();
            }
        }
    }

    private void OpenPauseMenu()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        gamePaused = true;
    }

    #region Button Methods

    public void OnSettingsOpen()
    {
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(true);
        settingsMenuOpen = true;
    }

    public void OnSettingsClose()
    {
        settingsMenu.SetActive(false);
        pauseMenu.SetActive(true);
        settingsMenuOpen = false;
    }

    public void Quit()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
        pauseMenu.SetActive(false);
    }

    public void Resume()
    {
        if (gamePaused)
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1f;
            gamePaused = false;
            settingsMenuOpen = false;
        }
    }

    #endregion
}

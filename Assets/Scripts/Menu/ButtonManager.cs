using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    public static ButtonManager instance;
    public static bool gamePaused;
    private bool mapIsOpen, paused;
    public GameObject pauseMenu;
    
    void Awake()
    {
        pauseMenu.SetActive(false);
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
        if (SceneManager.GetActiveScene().name != "User-Interface" && Input.GetKey(KeyCode.Escape))
        {
            if (gamePaused)
            {
                Resume();
            }
            else
            {
                OpenPauseMenu();
            }
        }
    }

    void OpenPauseMenu()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        gamePaused = true;
    }

    #region Button Methods

    public void Quit()
    {
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        SceneManager.LoadScene(0);
    }

    public void Resume()
    {
        if (gamePaused)
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1f;
            gamePaused = false;
        }
    }

    #endregion

    /*void OnGUI()
    {
        Listens to any key press, logic can be used for anything in future
        Event e = Event.current;
        if (e.isKey && e.keyCode != KeyCode.None)
        {
            // Logic
        }
    }*/

}

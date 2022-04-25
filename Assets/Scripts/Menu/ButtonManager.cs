using UnityEngine.SceneManagement;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    public static ButtonManager instance;
    public static bool gamePaused;
    public GameObject pauseMenu;

    void Awake()
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

    void Update()
    {
        CheckForPause();

        // Temporary cheat for debugging purposes
        if (Input.GetKey(KeyCode.L))
        {
            SceneManager.LoadScene(13);
        }
    }

    void CheckForPause()
    {
        if (SceneManager.GetActiveScene().name != "User-Interface" && Input.GetKeyDown(KeyCode.Escape))
        {
            if (gamePaused)
            {
                Resume();
            }
            else if (!gamePaused && Time.timeScale != 0)
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

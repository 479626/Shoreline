using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance;
    public GameObject questMenu;
    public static bool gamePaused, quests;

    void Awake()
    {
        questMenu.SetActive(false);
        Time.timeScale = 1f;
        quests = false;

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
        CheckForQuests();
    }

    void CheckForQuests()
    {
        if (SceneManager.GetActiveScene().name != "User-Interface" && Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.Escape))
        {
            if (quests)
            {
                Resume();
            }
            else if (!gamePaused && Time.timeScale != 0)
            {
                OpenQuestMenu();
            }
        }
    }

    void OpenQuestMenu()
    {
        questMenu.SetActive(true);
        Time.timeScale = 0f;
        gamePaused = true;
        quests = true;
    }

    public void Resume()
    {
        if (gamePaused)
        {
            questMenu.SetActive(false);
            Time.timeScale = 1f;
            quests = false;
            gamePaused = false;
        }
    }
}

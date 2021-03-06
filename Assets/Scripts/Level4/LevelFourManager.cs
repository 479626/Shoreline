using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelFourManager : MonoBehaviour
{
    public PlayerStats stats;
    public GameObject door;
    public GameObject sceneTransition;
    public DialogueInteraction dialogue;
    private bool completedLevel = false;

    private void Awake()
    {
        TriggerDoors();
        SoundManager.instance.FluteSound();
    }

    private void Update()
    {
        if (completedLevel) return;

        CheckForProgress();
    }

    private void CheckForProgress()
    {
        if (SceneManager.GetActiveScene().name == "L4-Beach")
        {
            if (stats.pirateKills >= 4 && stats.pirateCrewBossDeath)
            {
                completedLevel = true;
                door.SetActive(true);
                dialogue.GetComponent<DialogueInteraction>().TriggerDialogue(1);
            }
        }

        if (SceneManager.GetActiveScene().name == "L4-Ship")
        {
            if (stats.defeatedGateKeeper)
            {
                SoundManager.instance.DoorSound();
                completedLevel = true;
                sceneTransition.SetActive(true);
                door.SetActive(false);
            }
        }

        if (SceneManager.GetActiveScene().name == "L4-Battle")
        {
            if (stats.defeatedFinalBoss)
            {
                completedLevel = true;
            }
        }
    }

    private void TriggerDoors()
    {
        if (SceneManager.GetActiveScene().name == "L4-Beach" || SceneManager.GetActiveScene().name == "L4-Battle")
        {
            door.SetActive(false);
        }
        else
        {
            sceneTransition.SetActive(false);
        }
    }
}

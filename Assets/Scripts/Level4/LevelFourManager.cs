using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelFourManager : MonoBehaviour
{
    public PlayerStats stats;
    public GameObject door;
    public DialogueInteraction dialogue;
    private bool completedLevel = false;

    private void Start()
    {
        door.SetActive(false);
    }

    private void Update()
    {
        if (completedLevel) return;

        CheckForProgress();
    }

    private void CheckForProgress()
    {
        if (stats.pirateKills >= 4 && stats.pirateCrewBossDeath)
        {
            completedLevel = true;
            door.SetActive(true);
            dialogue.GetComponent<DialogueInteraction>().TriggerDialogue(1);
        }
    }
}

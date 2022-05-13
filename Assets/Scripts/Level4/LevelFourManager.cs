using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelFourManager : MonoBehaviour
{
    public PlayerStats stats;
    public GameObject door;

    private void Start()
    {
        door.SetActive(false);
    }

    private void Update()
    {
        CheckForProgress();
    }

    private void CheckForProgress()
    {
        if (stats.pirateKills >= 4 && stats.pirateCrewBossDeath)
        {
            door.SetActive(true);
        }
    }
}

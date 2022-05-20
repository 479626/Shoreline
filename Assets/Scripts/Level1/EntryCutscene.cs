using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class EntryCutscene : MonoBehaviour
{
    public int scene, cutscene;
    public Animator anim;
    public PlayerStats stats;
    public Vector2 playerPos;
    public VectorValue playerStorage;
    public Interaction interaction;

    private void MovePlayer()
    {
        if (SceneManager.GetActiveScene().name == "L1-Town")
        {
            if (!stats.seenLevelOneCutscene)
            {
                stats.seenLevelOneCutscene = true;
                SoundManager.instance.DoorSound();
                playerStorage.initialValue = playerPos;
                SceneManager.LoadScene(cutscene);
            }
            else
            {
                SoundManager.instance.DoorSound();
                playerStorage.initialValue = playerPos;
                SceneManager.LoadScene(scene);
            }
        }

        if (SceneManager.GetActiveScene().name == "L4-Beach")
        {
            if (stats.pirateKills != 4) return;
            SoundManager.instance.DoorSound();
            playerStorage.initialValue = playerPos;
            SceneManager.LoadScene(scene);
        }
    }

    #region Collision Detection

    public void OnTriggerStay2D(Collider2D col)
    {
        if (!col.CompareTag("Player") || col.isTrigger) return;

        interaction.InteractOn();
        if (SceneManager.GetActiveScene().buildIndex == 2 || SceneManager.GetActiveScene().buildIndex == 8)
        {
            anim.SetBool("Open", true);
        }
        if (Input.GetKey(KeyCode.F))
        {
            MovePlayer();
        }
    }

    public void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player") && !col.isTrigger)
        {
            anim.SetBool("Open", false);
            interaction.InteractOff();
        }
    }

    #endregion
}

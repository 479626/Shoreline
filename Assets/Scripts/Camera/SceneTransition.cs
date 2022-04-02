using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public int scene;
    public Vector2 playerPos;
    public VectorValue playerStorage;
    public Interaction interaction;


    public void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("Player") && !col.isTrigger)
        {
            interaction.InteractOn();

            if (Input.GetKey(KeyCode.F))
            {
                SoundManager.instance.DoorSound();
                playerStorage.initialValue = playerPos;
                SceneManager.LoadScene(scene);
            }
        }
    }

    public void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player") && !col.isTrigger)
        {
            interaction.InteractOff();
        }
    }
}

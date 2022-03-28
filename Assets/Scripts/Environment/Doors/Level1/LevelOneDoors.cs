using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelOneDoors : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        Interaction trigger = gameObject.GetComponent<Interaction>();
        trigger.InteractOn();

        if (Input.GetKey(KeyCode.F) && col.gameObject.name == "Player")
        {
            SceneManager.LoadScene("L1-House1");
            Interaction.InteractOn();
        }
    }
}

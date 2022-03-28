using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelOneDoors : MonoBehaviour
{
    public Interaction interaction;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.name == "Player")
        {
            interaction.InteractOn();
            Debug.Log("Found player");
            
            if(Input.GetKey(KeyCode.F))
            {
                SceneManager.LoadScene("L1-House1");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.name == "Player")
        {
            Debug.Log("Lost player");
            interaction.InteractOff();
        }
    }
}

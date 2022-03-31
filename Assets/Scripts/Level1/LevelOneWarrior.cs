using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelOneWarrior : MonoBehaviour
{
    public Interaction interaction;
    bool triggerDialogue;
    public GameObject dm;
    int used;

    void Start()
    {
        triggerDialogue = false;
    }

    void Update()
    {
        CheckForDialogue();
        if (used > 0 && dm.GetComponent<DialogueManager>().iFinished1 == true)
        {
            SceneManager.LoadScene("L1-Battle");
            dm.GetComponent<DialogueManager>().iFinished1 = false;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "Player")
        {
            if (used == 0)
            {
                interaction.InteractOn();
            }
            triggerDialogue = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.name == "Player")
        {
            interaction.InteractOff();
        }
    }

    void CheckForDialogue()
    {
        if (triggerDialogue == true)
        {
            if (Input.GetKey(KeyCode.F) && used == 0)
            {
                // If the player is in range of the NPC and it's thier first interaction, then trigger dialogue.
                DialogueInteraction trigger = gameObject.GetComponent<DialogueInteraction>();
                trigger.TriggerDialogue(1);
                triggerDialogue = false;
                interaction.InteractOff();
            }

            if (Input.GetKey(KeyCode.G) && used == 0)
            {
                // If the player is in range of the NPC and it's thier first interaction, then trigger dialogue.
                DialogueInteraction trigger = gameObject.GetComponent<DialogueInteraction>();
                trigger.TriggerDialogue(2);
                triggerDialogue = false;
                interaction.InteractOff();
            }
        }
    }
}

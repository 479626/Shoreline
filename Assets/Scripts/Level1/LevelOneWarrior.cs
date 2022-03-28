using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LevelOneWarrior : MonoBehaviour
{
    public Interaction interaction;
    bool triggerDialogue;
    int used;

    void Start()
    {
        triggerDialogue = false;
    }

    void Update()
    {
        CheckForDialogue();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.name == "Player")
        {
            interaction.InteractOn();
            triggerDialogue = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if(col.gameObject.name == "Player")
        {
            interaction.InteractOff();
        }
    }

    void CheckForDialogue()
    {
        if(triggerDialogue == true)
        {
            if(Input.GetKey(KeyCode.F) && used == 0)
            {
                // If the player is in range of the NPC and it's thier first interaction, then trigger dialogue.
                used++;
                DialogueInteraction trigger = gameObject.GetComponent<DialogueInteraction>();
                trigger.TriggerDialogue();
                interaction.InteractOff();
            }
        }
    }
}

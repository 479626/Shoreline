using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LevelOneWarrior : MonoBehaviour
{
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
            triggerDialogue = true;
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
            }
        }
    }
}

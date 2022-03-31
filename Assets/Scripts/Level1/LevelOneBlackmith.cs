using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelOneBlackmith : MonoBehaviour
{
    public Interaction interaction;
    bool triggerDialogue;

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
        if (col.gameObject.name == "Player")
        {
            interaction.InteractOn();
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
            if (Input.GetKey(KeyCode.F))
            {
                DialogueInteraction trigger = gameObject.GetComponent<DialogueInteraction>();
                trigger.TriggerDialogue(1);
                triggerDialogue = false;
                interaction.InteractOff();
            }
        }
    }

}

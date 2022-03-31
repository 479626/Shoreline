using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueInteractor : MonoBehaviour
{
    public Interaction interaction;
    bool triggerDialogue;
    public GameObject dialogueManager;
    public InteractionCounter count;
    int used;
    private Rigidbody2D rb;

    void Start()
    {
        triggerDialogue = false;
        rb = GetComponent<Rigidbody2D>();
        used = 0;
    }

    void Update()
    {
        CheckForDialogue();
        if (used > 0 && dialogueManager.GetComponent<DialogueManager>().finishedDialogue == true)
        {
            dialogueManager.GetComponent<DialogueManager>().finishedDialogue = false;
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
                used++;
                DialogueInteraction trigger = gameObject.GetComponent<DialogueInteraction>();
                trigger.TriggerDialogue(1);
                triggerDialogue = false;
                interaction.InteractOff();
                count.levelOne++;
            }
        }
    }
}

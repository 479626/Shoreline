using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelOneWarrior : MonoBehaviour
{
    public Interaction interaction;
    bool triggerDialogue, interactionThreshold, finalMessage;
    public GameObject dialogueManager;
    public int nextLevelScene;
    public InteractionCounter count;

    void Start()
    {
        triggerDialogue = false;
        interactionThreshold = false;
        finalMessage = false;
        dialogueManager.GetComponent<DialogueManager>().finishedDialogue = false;
    }

    void Update()
    {
        CheckForDialogue();
        if (finalMessage == true && dialogueManager.GetComponent<DialogueManager>().finishedDialogue == true)
        {
            Debug.Log("COMPLETED DIALOGUE TRACKS & READY TO MOVE ON");
            SceneManager.LoadScene(nextLevelScene);
            dialogueManager.GetComponent<DialogueManager>().finishedDialogue = false;
        }
        if (count.levelOne > 3)
        {
            interactionThreshold = true;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "Player")
        {
            interaction.InteractOn();

            if (interactionThreshold)
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
            DialogueInteraction trigger = gameObject.GetComponent<DialogueInteraction>();

            if (Input.GetKey(KeyCode.F) && interactionThreshold == false)
            {
                // If the player is in range of the NPC and it's thier first interaction, then trigger dialogue.
                trigger.TriggerDialogue(1);
                triggerDialogue = false;
                interaction.InteractOff();
                dialogueManager.GetComponent<DialogueManager>().finishedDialogue = false;
            }

            if (Input.GetKey(KeyCode.F) && interactionThreshold == true)
            {
                // If the player is in range of the NPC and it's thier first interaction, then trigger dialogue.
                trigger.TriggerDialogue(2);
                triggerDialogue = false;
                interactionThreshold = false;
                finalMessage = true;
                interaction.InteractOff();
            }
        }
    }
}

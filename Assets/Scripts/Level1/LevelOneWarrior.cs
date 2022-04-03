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
        if (finalMessage && dialogueManager.GetComponent<DialogueManager>().finishedDialogue == true)
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
            triggerDialogue = true;

            if (interactionThreshold)
            {
                interaction.InteractOn();
            }
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.name == "Player")
        {
            interaction.InteractOff();
            triggerDialogue = false;
        }
    }

    void CheckForDialogue()
    {
        if (triggerDialogue == true && Input.GetKey(KeyCode.F))
        {
            if (interactionThreshold)
            {
                Debug.Log("Triggered dialogue 2");
                gameObject.GetComponent<DialogueInteraction>().TriggerDialogue(2);
                triggerDialogue = false;
                interactionThreshold = false;
                finalMessage = true;
                interaction.InteractOff();
            }
            else
            {
                Debug.Log("Triggered dialogue 1");
                gameObject.GetComponent<DialogueInteraction>().TriggerDialogue(1);
                triggerDialogue = false;
                interaction.InteractOff();
                dialogueManager.GetComponent<DialogueManager>().finishedDialogue = false;
            }
        }
    }
}

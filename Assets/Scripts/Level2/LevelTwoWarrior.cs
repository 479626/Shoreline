using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelTwoWarrior : MonoBehaviour
{

    bool triggerDialogue, defeated;
    public GameObject dialogueManager;
    public int nextLevelScene;

    void Start()
    {
        triggerDialogue = false;
        defeated = false;
        dialogueManager.GetComponent<DialogueManager>().finishedDialogue = false;
    }

    void Update()
    {
        CheckForDialogue();
        if (defeated == true)
        {
            Debug.Log("Warrior has been defeated");
            SceneManager.LoadScene(nextLevelScene);
            dialogueManager.GetComponent<DialogueManager>().finishedDialogue = false;
        }
    }

    void CheckForDialogue()
    {
        DialogueInteraction trigger = gameObject.GetComponent<DialogueInteraction>();

        if (triggerDialogue == true)
        {
            // Sentences here
        }
    }
}

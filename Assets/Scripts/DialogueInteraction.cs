using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueInteraction : MonoBehaviour
{
    public Dialogue dialogueOne;
    public Dialogue dialogueTwo;
    public Dialogue dialogueThree;

    public void TriggerDialogue(int id)
    {
        if (id == 1)
        {
            FindObjectOfType<DialogueManager>().StartDialogue(dialogueOne);
        }
        if (id == 2)
        {
            FindObjectOfType<DialogueManager>().StartDialogue(dialogueTwo);
        }
        if (id == 3)
        {
            FindObjectOfType<DialogueManager>().StartDialogue(dialogueThree);
        }
    }
}

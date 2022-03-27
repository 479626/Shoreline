using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueInteraction : MonoBehaviour
{
    public Dialogue dialogue;

    public void TriggerDialogue()
    {
        Debug.Log("Click registered");
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }

}

using UnityEngine;

public class DialogueInteraction : MonoBehaviour
{
    public Dialogue dialogueOne, dialogueTwo, dialogueThree;

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

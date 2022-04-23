using UnityEngine;

public class LevelOneLock : MonoBehaviour
{
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
        if (col.CompareTag("Player"))
        {
            triggerDialogue = true;
        }
    }

    void CheckForDialogue()
    {
        if (triggerDialogue)
        {
            DialogueInteraction trigger = gameObject.GetComponent<DialogueInteraction>();
            trigger.TriggerDialogue(1);
            triggerDialogue = false;
        }
    }
}
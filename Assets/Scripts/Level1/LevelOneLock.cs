using UnityEngine;

public class LevelOneLock : MonoBehaviour
{
    private bool triggerDialogue;

    private void Start()
    {
        triggerDialogue = false;
    }

    private void Update()
    {
        CheckForDialogue();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            triggerDialogue = true;
        }
    }

    private void CheckForDialogue()
    {
        if (!triggerDialogue) return;
        
        var trigger = gameObject.GetComponent<DialogueInteraction>();
        trigger.TriggerDialogue(1);
        triggerDialogue = false;
    }
}
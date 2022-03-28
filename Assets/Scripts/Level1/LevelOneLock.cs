using System.Collections;
using System.Collections.Generic;
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
        if (col.gameObject.name == "Player")
        {
            triggerDialogue = true;
        }
    }

    void CheckForDialogue()
    {
        if (triggerDialogue == true)
        {
            DialogueInteraction trigger = gameObject.GetComponent<DialogueInteraction>();
            trigger.TriggerDialogue();
        }
    }
}

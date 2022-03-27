using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelOneWarrior : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Debug.Log("Click registered");
            FindObjectOfType<DialogueInteraction>().TriggerDialogue();
        }
    }
}

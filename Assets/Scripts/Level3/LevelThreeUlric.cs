using UnityEngine;

public class LevelThreeUlric : MonoBehaviour
{
    public Interaction interaction;
    private bool triggerDialogue;
    public GameObject dialogueManager;
    public InteractionCounter count;
    public PlayerStats stats;

    private void Awake()
    {
        Debug.Log("Recognised Level 1 and saved progress");
        stats.currentLevel = 1;
    }


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
            interaction.InteractOn();
            triggerDialogue = true;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            interaction.InteractOff();
            triggerDialogue = false;
        }
    }

    private void CheckForDialogue()
    {
        if (triggerDialogue && Input.GetKey(KeyCode.F))
        {
            gameObject.GetComponent<DialogueInteraction>().TriggerDialogue(1);
            triggerDialogue = false;
            interaction.InteractOff();
            dialogueManager.GetComponent<DialogueManager>().finishedDialogue = false;
        }
    }
}

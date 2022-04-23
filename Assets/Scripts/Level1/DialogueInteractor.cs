using UnityEngine;

public class DialogueInteractor : MonoBehaviour
{
    public Interaction interaction;
    bool triggerDialogue;
    public GameObject dialogueManager;
    public InteractionCounter count;
    int used;
    private Rigidbody2D rb;

    void Start()
    {
        triggerDialogue = false;
        rb = GetComponent<Rigidbody2D>();
        used = 0;
    }

    void Update()
    {
        CheckForDialogue();
        if (used > 0 && dialogueManager.GetComponent<DialogueManager>().finishedDialogue == true)
        {
            dialogueManager.GetComponent<DialogueManager>().finishedDialogue = false;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            if (used == 0)
            {
                interaction.InteractOn();
            }
            triggerDialogue = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            interaction.InteractOff();
        }
    }

    void CheckForDialogue()
    {
        if (triggerDialogue)
        {
            if (Input.GetKey(KeyCode.F) && used == 0)
            {
                used++;
                gameObject.GetComponent<DialogueInteraction>().TriggerDialogue(1);
                triggerDialogue = false;
                interaction.InteractOff();
                count.levelOne++;
            }
        }
    }
}

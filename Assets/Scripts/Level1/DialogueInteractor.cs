using UnityEngine;

public class DialogueInteractor : MonoBehaviour
{
    public Interaction interaction;
    private bool triggerDialogue;
    public GameObject dialogueManager;
    public InteractionCounter count;
    public string npcName;
    public bool used;
    private Rigidbody2D rb;

    void Start()
    {
        triggerDialogue = false;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        CheckForDialogue();
        if (used && dialogueManager.GetComponent<DialogueManager>().finishedDialogue == true)
        {
            dialogueManager.GetComponent<DialogueManager>().finishedDialogue = false;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            CheckIfUsed();
            Debug.Log("[" + this.gameObject.name + "] used: " + used);
            if (!used)
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
            if (Input.GetKey(KeyCode.F) && !used)
            {
                AddInteractionCount();
                gameObject.GetComponent<DialogueInteraction>().TriggerDialogue(1);
                triggerDialogue = false;
                interaction.InteractOff();
                count.levelOne++;
            }
        }
    }

    void CheckIfUsed()
    {
        switch (npcName)
        {
            case "Beth":
                if (count.npcBeth == 1)
                    used = true;
                break;
            case "Pete":
                if (count.npcPete == 1)
                    used = true;
                break;
            case "Pete Jr":
                if (count.npcPeteJr == 1)
                    used = true;
                break;
            case "Mary":
                if (count.npcMary == 1)
                    used = true;
                break;
            case "Anne":
                if (count.npcAnne == 1)
                    used = true;
                break;
            case "Christopher":
                if (count.npcChristopher == 1)
                    used = true;
                break;
            case "Timmy":
                if (count.npcTimmy == 1)
                    used = true;
                break;
            case "Gary":
                if (count.npcGary == 1)
                    used = true;
                break;
            case "Ulric":
                if (count.npcUlric == 1)
                    used = true;
                break;
        }
    }

    void AddInteractionCount()
    {
        switch (npcName)
        {
            case "Beth":
                count.npcBeth++;
                break;
            case "Pete":
                count.npcPete++;
                break;
            case "Pete Jr":
                count.npcPeteJr++;
                break;
            case "Mary":
                count.npcMary++;
                break;
            case "Anne":
                count.npcAnne++;
                break;
            case "Christopher":
                count.npcChristopher++;
                break;
            case "Timmy":
                count.npcTimmy++;
                break;
            case "Gary":
                count.npcGary++;
                break;
            case "Ulric":
                count.npcUlric++;
                break;
        }
    }
}

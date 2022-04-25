using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelOneWarrior : MonoBehaviour
{
    public Interaction interaction;
    bool triggerDialogue, interactionThreshold, finalMessage;
    public GameObject dialogueManager;
    public int nextLevelScene;
    public InteractionCounter count;
    public PlayerStats stats;

    void Awake()
    {
        Debug.Log("Recognised Level 1 and saved progress");
        stats.currentLevel = 1;
    }


    void Start()
    {
        triggerDialogue = false;
        interactionThreshold = false;
        finalMessage = false;
        dialogueManager.GetComponent<DialogueManager>().finishedDialogue = false;
    }

    void Update()
    {
        CheckForDialogue();
        if (finalMessage && dialogueManager.GetComponent<DialogueManager>().finishedDialogue == true)
        {
            SceneManager.LoadScene(nextLevelScene);
            dialogueManager.GetComponent<DialogueManager>().finishedDialogue = false;
        }
        if (count.levelOne > 3)
        {
            interactionThreshold = true;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            interaction.InteractOn();
            triggerDialogue = true;

            if (interactionThreshold)
            {
                interaction.InteractOn();
            }
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            interaction.InteractOff();
            triggerDialogue = false;
        }
    }

    void CheckForDialogue()
    {
        if (triggerDialogue && Input.GetKey(KeyCode.F))
        {
            if (interactionThreshold)
            {
                gameObject.GetComponent<DialogueInteraction>().TriggerDialogue(2);
                triggerDialogue = false;
                interactionThreshold = false;
                finalMessage = true;
                interaction.InteractOff();
            }
            else
            {
                gameObject.GetComponent<DialogueInteraction>().TriggerDialogue(1);
                triggerDialogue = false;
                interaction.InteractOff();
                dialogueManager.GetComponent<DialogueManager>().finishedDialogue = false;
            }
        }
    }
}

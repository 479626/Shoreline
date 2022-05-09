using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelThreeWarriorController : MonoBehaviour
{
    public Interaction interaction;
    private bool triggerDialogue, interactionThreshold, finalMessage;
    public int nextLevelScene;
    public GameObject dialogueManager;
    public InteractionCounter count;
    public PlayerStats stats;

    private void Awake()
    {
        if (SceneManager.GetActiveScene().name == "L1-Town")
        {
            stats.currentLevel = 1;
        }
    }


    private void Start()
    {
        triggerDialogue = false;
        interactionThreshold = false;
        finalMessage = false;
        dialogueManager.GetComponent<DialogueManager>().finishedDialogue = false;
    }

    private void Update()
    {
        CheckForDialogue();
        MovePlayer();
    }

    private void MovePlayer()
    {
        if (finalMessage && gameObject.GetComponent<DialogueManager>().finishedDialogue == true)
        {
            SceneManager.LoadScene(nextLevelScene);
            dialogueManager.GetComponent<DialogueManager>().finishedDialogue = false;
        }

        if (count.npcUlric == 1 || stats.crabKills >= 3)
        {
            interactionThreshold = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.CompareTag("Player")) return;
        interaction.InteractOn();
        triggerDialogue = true;

        if (interactionThreshold)
        {
            interaction.InteractOn();
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (!col.CompareTag("Player")) return;
        interaction.InteractOff();
        triggerDialogue = false;
    }

    private void CheckForDialogue()
    {
        if (triggerDialogue && Input.GetKey(KeyCode.F))
        {
            if (count.npcUlric == 1)
            {
                gameObject.GetComponent<DialogueInteraction>().TriggerDialogue(2);
                count.levelThreeWarrior = 1;
            }
            else
            {
                gameObject.GetComponent<DialogueInteraction>().TriggerDialogue(1);
                count.levelThreeWarrior = 2;
            }

            if (stats.crabKills > 2 && count.npcUlric == 1)
            {
                gameObject.GetComponent<DialogueInteraction>().TriggerDialogue(3);
                count.levelThreeWarrior = 3;
                finalMessage = true;
            }

            triggerDialogue = false;
            interaction.InteractOff();
            dialogueManager.GetComponent<DialogueManager>().finishedDialogue = false;
        }
    }
}
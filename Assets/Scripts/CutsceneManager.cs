using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneManager : MonoBehaviour
{
    [Header("Dialogue")]
    public Dialogue cutsceneDialogue;
    public GameObject dialogueManager;
    [SerializeField] private bool hasDialogue, dialogueInProgress;
    [SerializeField] float cutsceneDialogueStart;

    [Header("Cutscene Transition")]
    [SerializeField] private int nextSceneIndex;
    [SerializeField] private float cutsceneLength;

    private void Update()
    {
        cutsceneLength -= Time.deltaTime;
        cutsceneDialogueStart -= Time.deltaTime;
        
        if (hasDialogue && cutsceneDialogueStart <= 0)
        {
            CheckForDialogue();
        }
        
        if (cutsceneLength <= 0 && dialogueManager.GetComponent<DialogueManager>().finishedDialogue)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
    }

    private void CheckForDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(cutsceneDialogue);
        hasDialogue = false;
    }
}

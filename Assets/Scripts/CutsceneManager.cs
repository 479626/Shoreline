using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneManager : MonoBehaviour
{
    [Header("Dialogue")]
    public Dialogue cutsceneDialogue;
    public GameObject dialogueManager;
    public Vector2 playerPos;
    public VectorValue playerStorage;
    [SerializeField] private bool hasDialogue, hasTeleportLocation, dialogueInProgress;
    private bool finishedDialogue;
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
        
        if (cutsceneLength <= 0)
        {
            if (hasDialogue) return;

            if (hasTeleportLocation)
            {
                MovePlayer();
            }
            else
            {
                SceneManager.LoadScene(nextSceneIndex);
            }
        }
    }

    private void MovePlayer()
    {
        playerStorage.initialValue = playerPos;
        SceneManager.LoadScene(nextSceneIndex);
    }

    //dialogueManager.GetComponent<DialogueManager>().finishedDialogue
    private void CheckForDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(cutsceneDialogue);
        hasDialogue = false;
        finishedDialogue = true;
    }
}

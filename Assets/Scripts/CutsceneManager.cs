using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneManager : MonoBehaviour
{
    [Header("Dialogue")]
    public Dialogue cutsceneDialogue;
    public GameObject dialogueManager;
    public Vector2 playerPos;
    public VectorValue playerStorage;
    private DialogueManager _dialogueManager;
    [SerializeField] private bool hasDialogue, hasSpoken, hasTeleportLocation, dialogueInProgress;
    [SerializeField] private float cutsceneDialogueStart;

    [Header("Cutscene Transition")]
    [SerializeField] private int nextSceneIndex;
    [SerializeField] private float cutsceneLength;

    private void Awake()
    {
        hasSpoken = false;
        _dialogueManager = dialogueManager.GetComponent<DialogueManager>();
    }

    private void Update()
    {
        Timer();
        CheckConditions();
    }

    private void Timer()
    {
        cutsceneLength -= Time.deltaTime;
        cutsceneDialogueStart -= Time.deltaTime;
    }

    private void CheckConditions()
    {
        if (hasDialogue)
        {
            if (!hasSpoken)
            {
                CheckForDialogue();
            }

            if (!_dialogueManager.finishedDialogue) return;
            MovePlayer();
        }
        else
        {
            if (cutsceneLength <= 0)
            {
                MovePlayer();
            }
        }
    }

    private void MovePlayer()
    {
        if (hasTeleportLocation)
        {
            playerStorage.initialValue = playerPos;
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
    }

    private void CheckForDialogue()
    {
        if (cutsceneDialogueStart <= 0)
        {
            hasSpoken = true;
            _dialogueManager.StartDialogue(cutsceneDialogue);
        }
    }
}

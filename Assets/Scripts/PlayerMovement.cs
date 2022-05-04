using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public Rigidbody2D rb;
    public Animator animator;
    public PlayerStats stats;
    public VectorValue startingPosition;
    private static GameObject player;
    private Vector2 movement;
    private DialogueManager dialogueManager;

    private void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();
        transform.position = startingPosition.initialValue;
    }

    private void Update()
    {
        MoveLogic();
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * (speed * Time.fixedDeltaTime));
    }

    public void PlayWalkSound()
    {
        SoundManager.instance.WalkSound();
    }

    private void MoveLogic()
    {
        speed = 3.5f + stats.speedModifier;
        if (Time.timeScale != 0f && !dialogueManager.dialogueInProgress)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");

            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
            animator.SetFloat("Speed", movement.sqrMagnitude);
        }

        if (!dialogueManager.dialogueInProgress) return;
        
        animator.SetFloat("Speed", 0);
        movement.x = 0;
        movement.y = 0;
    }

}

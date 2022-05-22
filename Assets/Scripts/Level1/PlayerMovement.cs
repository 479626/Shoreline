using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public Rigidbody2D rb;
    public Animator animator;
    public PlayerStats stats;
    public VectorValue startingPosition;
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

    public void PlayWalkSound()
    {
        SoundManager.instance.WalkSound();
    }

    private void MoveLogic()
    {
        var speed = 3.5f + stats.speedModifier;
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            speed = 2f + stats.speedModifier;
        }
        else
        {
            speed = 3.5f + stats.speedModifier;
        }
        var movementVector = Vector2.ClampMagnitude(movement, 1);
        var newPosition = rb.position + speed * Time.fixedDeltaTime * movementVector;

        if (Time.timeScale != 0f && !dialogueManager.dialogueInProgress)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");

            rb.MovePosition(newPosition);

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

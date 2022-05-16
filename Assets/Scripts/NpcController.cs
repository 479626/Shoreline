using UnityEngine;

public class NpcController : MonoBehaviour
{
    public float speed;
    public bool isWalking;
    private float waitCounter, walkCounter;
    public float waitTime, walkTime;
    private int walkDirection;
    private int blockDirection = -1;
    private bool playerIsNear = false;

    private Rigidbody2D rb;
    public Animator animator;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        waitCounter = waitTime;
        walkCounter = walkTime;

        Direction();
    }

    private void Update()
    {
        ArtificialIntelligence();
    }

    private void ArtificialIntelligence()
    {
        if (isWalking && !playerIsNear)
        {
            walkCounter -= Time.deltaTime;

            switch (walkDirection)
            {
                case 0:
                    rb.velocity = new Vector2(0, speed);
                    break;
                case 1:
                    rb.velocity = new Vector2(speed, 0);
                    break;
                case 2:
                    rb.velocity = new Vector2(0, -speed);
                    break;
                case 3:
                    rb.velocity = new Vector2(-speed, 0);
                    break;
            }

            animator.SetFloat("X", rb.velocity.x);
            animator.SetFloat("Y", rb.velocity.y);

            if (walkCounter < 0)
            {
                isWalking = false;
                waitCounter = waitTime;
            }
            animator.SetBool("moving", isWalking);
        }

        else if (walkCounter > 0)
        {
            rb.velocity = new Vector2(0, 0);
            walkCounter -= Time.deltaTime;
            isWalking = false;
            animator.SetBool("moving", isWalking);
        }
        else
        {
            waitCounter -= Time.deltaTime;

            rb.velocity = Vector2.zero;
            if (waitCounter < 0)
            {
                Direction();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Wall") || col.gameObject.CompareTag("Enemy"))
        {
            isWalking = false;
            animator.SetBool("moving", isWalking);
            rb.velocity = Vector2.zero;
            blockDirection = walkDirection;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            playerIsNear = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            playerIsNear = true;
        }
    }

    private void Direction()
    {
        if (blockDirection != -1)
        {
            walkDirection = Random.Range(0, 3);
            if (walkDirection == blockDirection)
            {
                walkDirection = 3;
            }
            blockDirection = -1;
        }
        else
        {
            walkDirection = Random.Range(0, 4);
        }
        isWalking = true;
        walkCounter = walkTime;
    }

    public void PlayWalkSound()
    {
        SoundManager.instance.WalkSound();
    }
}

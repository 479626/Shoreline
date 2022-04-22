using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 3.5f;
    public Rigidbody2D rb;
    public Animator animator;
    public VectorValue startingPosition;
    private static GameObject player;
    Vector2 movement;

    void Start()
    {
        transform.position = startingPosition.initialValue;
    }

    void Update()
    {
        MoveLogic();
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }

    public void PlayWalkSound()
    {
        SoundManager.instance.WalkSound();
    }

    void MoveLogic()
    {
        if (!ButtonManager.gamePaused)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");

            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
            animator.SetFloat("Speed", movement.sqrMagnitude);
        }
    }

}

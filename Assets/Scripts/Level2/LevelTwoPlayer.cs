using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTwoPlayer : MonoBehaviour
{
    public float speed = 3.5f;
    public Rigidbody2D rb;
    public Animator animator;
    private static GameObject player;
    Vector2 movement;

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }

}

using UnityEngine;

public class UserInterface : MonoBehaviour
{
    Vector2 velocity;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        velocity.x = 6;
    }

    void Update()
    {
        rb.velocity = velocity;
    }
}

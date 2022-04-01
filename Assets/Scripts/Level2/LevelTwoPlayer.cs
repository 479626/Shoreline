using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTwoPlayer : MonoBehaviour
{
    private static GameObject player;
    public Rigidbody2D rb;
    public Animator animator;
    public Transform range;
    public LayerMask enemyLayer;
    public HealthBar healthBar;
    public GameObject warrior;
    Vector2 movement;

    public float attackRange = 0.5f;
    public float speed = 3.5f;
    public int maxHealth, currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (Input.GetButtonDown("Fire1"))
            Attack();
        else MoveLogic();
    }

    void MoveLogic()
    {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
    }

    void Attack()
    {
        int damage = Random.Range(8, 10);
        StartCoroutine(Attacking());
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(range.position, attackRange, enemyLayer);
        foreach(Collider2D enemy in hitEnemies)
        {
            warrior.GetComponent<LevelTwoWarrior>().TakeDamage(damage);
        }
    }

    IEnumerator Attacking()
    {
        speed = 0f;
        animator.SetTrigger("Attack");
        yield return new WaitForSeconds(0.5f);
        speed = 3.5f;
        yield break;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);
    }

    void OnDrawGizmos()
    {
        if (range == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(range.position, attackRange);
    }

}

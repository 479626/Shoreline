using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : MonoBehaviour
{
    public int maxHealth, currentHealth, damage;
    public HealthBar healthBar;
    public Animator animator;
    public DropManager dropManager;
    public bool dead = false;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    void Update()
    {
        if (currentHealth < 0)
        {
            dead = true;
            StartCoroutine(Die());
        }
    }

    IEnumerator Die()
    {
        animator.SetBool("death", true);
        yield return new WaitForSeconds(1.4f);
        for (int i = 0; i < 3; i++)
        {
            dropManager.SpawnCoin("Coin (Skeleton)", transform.position.x, transform.position.y);
        }
        Destroy(gameObject);
        yield break;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);
    }
}

using System.Collections;
using UnityEngine;

public class CrabController : MonoBehaviour
{
    public int maxHealth, currentHealth, damage;
    public HealthBar healthBar;
    public Animator animator;
    public PlayerStats playerStats;
    public DropManager dropManager;
    public bool dead = false;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    private void Update()
    {
        if (!dead)
        {
            CheckForDeath();
        }
    }

    private IEnumerator Die()
    {
        playerStats.crabKills++;
        animator.SetTrigger("death");
        yield return new WaitForSeconds(0.25f);
        dropManager.SpawnCoin("Coin (Crab)", transform.position.x, transform.position.y);
        Destroy(gameObject);
        yield return null;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);
    }

    private void CheckForDeath()
    {
        if (currentHealth >= 0) return;

        dead = true;
        if (!dead) return;
        StartCoroutine(Die());
        dead = false;
    }
}

using System.Collections;
using UnityEngine;

public class SkeletonController : MonoBehaviour
{
    public int maxHealth, currentHealth, damage;
    public HealthBar healthBar;
    public Animator animator;
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
        animator.SetBool("death", true);
        yield return new WaitForSeconds(1.4f);
        for (int i = 0; i < 3; i++)
        {
            dropManager.SpawnCoin("Coin (Skeleton)", transform.position.x, transform.position.y);
        }
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
        if (dead)
            StartCoroutine(Die());
    }
}

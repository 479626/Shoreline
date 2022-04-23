using System.Collections;
using UnityEngine;

public class Crab : MonoBehaviour
{
    public int maxHealth, currentHealth, damage;
    public HealthBar healthBar;
    public Animator animator;
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
        yield return new WaitForSeconds(0.25f);
        Destroy(gameObject);
        yield break;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);
    }
}

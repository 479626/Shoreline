using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AttackingPirate : MonoBehaviour
{
    public int maxHealth, currentHealth, damage;
    public PlayerStats stats;
    public HealthBar healthBar;
    public Animator animator;
    public EnemyController enemyController;
    public bool dead, isDead = false;

    private void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    private void Update()
    {
        if (!isDead)
        {
            CheckHealth();
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        SoundManager.instance.AttackSound();

        healthBar.SetHealth(currentHealth);
    }

    private void CheckHealth()
    {
        if (currentHealth < 0)
        {
            isDead = true;
            dead = true;
            enemyController.allowedToAttack = false;
            StartCoroutine(Die());
        }
    }

    private IEnumerator Die()
    {
        animator.SetBool("dead", true);
        if (isDead)
        {
            stats.pirateKills++;
        }
        isDead = false;
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
        yield return null;
    }

    public void PlaySound(string id)
    {
        if (id == "walk")
        {
            SoundManager.instance.WalkSound();
        }
        if (id == "swing")
        {
            SoundManager.instance.SwingSound();
        }
    }
}
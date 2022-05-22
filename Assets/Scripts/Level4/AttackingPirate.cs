using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        if (!isDead)
        {
            currentHealth -= damage;
            SoundManager.instance.AttackSound();

            healthBar.SetHealth(currentHealth);
        }
    }

    private void CheckHealth()
    {
        if (currentHealth < 0)
        {
            isDead = true;
            dead = true;
            enemyController.allowedToAttack = false;

            if (!isDead) return;
            StartCoroutine(Die());
            dead = false;
        }
    }

    private IEnumerator Die()
    {
        enemyController.allowedToAttack = false;
        animator.SetBool("dead", true);
        stats.pirateKills++;
        if (SceneManager.GetActiveScene().name != "L4-Ship") yield return null;
        stats.defeatedGateKeeper = true;
        yield return new WaitForSeconds(2f);
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
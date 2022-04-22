using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTwoPlayer : MonoBehaviour
{
    private static GameObject player;

    [Header("Movement")]
    public float speed = 3.5f;
    public Rigidbody2D rb;
    public Animator animator;
    Vector2 movement;

    [Header("Combat")]
    public float attackRange = 0.5f;
    public float attackRate = 1f;
    public float nextAttackTime = 0f;
    public int minDamage, maxDamage;
    public GameObject enemy;
    public Transform range;
    public LayerMask enemyLayer;

    [Header("Health")]
    public bool dead = false;
    public int maxHealth, currentHealth;
    public GameObject deathEffect;
    public HealthBar healthBar;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        healthBar.SetHealth(20);
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (Input.GetButtonDown("Fire1") && !dead && speed != 0)
        {
            if (Time.time >= nextAttackTime)
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
        else
        {
            MoveLogic();
        }

        if (currentHealth < 0)
        {
            dead = true;
            StartCoroutine(Death());
        }
    }

    IEnumerator Death()
    {
        speed = 0f;
        animator.SetBool("isDead", true);
        yield return new WaitForSeconds(1f);
        deathEffect.GetComponent<Restart>().Death();
        yield break;
    }

    void MoveLogic()
    {
        if (!ButtonManager.gamePaused)
        {
            rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);

            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
            animator.SetFloat("Speed", movement.sqrMagnitude);
        }
    }

    void Attack()
    {
        int damage = Random.Range(minDamage, maxDamage);
        StartCoroutine(Attacking());
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(range.position, attackRange, enemyLayer);
        foreach(Collider2D enemy in hitEnemies)
        {
            if (SceneManager.GetActiveScene().name == "L2-Battle")
            {
                enemy.GetComponent<LevelTwoWarrior>().TakeDamage(damage);
            }
            if (SceneManager.GetActiveScene().name == "Cove")
            {
                enemy.GetComponent<Crab>().TakeDamage(damage);
            }
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
        SoundManager.instance.AttackSound();

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

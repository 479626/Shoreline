using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTwoPlayer : MonoBehaviour
{
    private static GameObject player;

    [Header("Movement")]
    public float speed;
    public PlayerStats stats;
    public Rigidbody2D rb;
    public Animator animator;
    Vector2 movement;

    [Header("Combat")]
    public float attackRange = 0.5f;
    public float attackRate = 1f;
    public float nextAttackTime = 0f;
    public bool attacking = false;
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
        FindObjectOfType<DialogueManager>().dialogueInProgress = false;
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (Input.GetButtonDown("Fire1") && !dead && speed != 0)
        {
            if (Time.time >= nextAttackTime && !FindObjectOfType<DialogueManager>().dialogueInProgress)
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
        dead = true;
        animator.SetBool("isDead", true);
        yield return new WaitForSeconds(1f);
        deathEffect.GetComponent<Restart>().Death();
        yield break;
    }

    void MoveLogic()
    {
        if (!dead && !attacking && Time.timeScale != 0f && !FindObjectOfType<DialogueManager>().dialogueInProgress)
        {
            speed = 3.5f + stats.speedModifier;
            rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);

            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
            animator.SetFloat("Speed", movement.sqrMagnitude);
        }

        if (FindObjectOfType<DialogueManager>().dialogueInProgress)
        {
            animator.SetFloat("Speed", 0);
            movement.x = 0;
            movement.y = 0;
        }
    }

    void Attack()
    {
        int damage = Random.Range(minDamage, maxDamage) + stats.damageBonus;
        StartCoroutine(Attacking());
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(range.position, attackRange, enemyLayer);
        foreach (Collider2D enemy in hitEnemies)
        {
            if (SceneManager.GetActiveScene().name == "L2-Battle")
            {
                enemy.GetComponent<LevelTwoWarrior>().TakeDamage(damage);
            }
            if (SceneManager.GetActiveScene().name == "Cove")
            {
                if (enemy.CompareTag("Skeleton"))
                {
                    enemy.GetComponent<Skeleton>().TakeDamage(damage);
                }
                if (enemy.CompareTag("Crab"))
                {
                    enemy.GetComponent<Crab>().TakeDamage(damage);
                }
            }
        }
    }

    IEnumerator Attacking()
    {
        attacking = true;
        animator.SetTrigger("Attack");
        yield return new WaitForSeconds(0.5f);
        attacking = false;
        yield return null;
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

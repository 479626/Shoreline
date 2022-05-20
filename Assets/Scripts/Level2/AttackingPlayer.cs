using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AttackingPlayer : MonoBehaviour
{
    private static GameObject player;

    [Header("Movement")]
    public float speed;
    public PlayerStats stats;
    public Rigidbody2D rb;
    public Animator animator;
    private Vector2 movement;

    [Header("Combat")]
    public float attackRange = 0.5f;
    public float attackRate = 1f;
    public float nextAttackTime = 0f;
    public bool attacking = false;
    public int minDamage, maxDamage;
    public Transform range;
    public LayerMask enemyLayer;
    public DamageIndicator damageIndicator;

    [Header("Health")]
    public bool dead = false;
    public int maxHealth, currentHealth;
    public GameObject deathEffect;
    public HealthBar healthBar;
    public bool canHeal;
    public int healAmount, healDelay;
    public float healTimer;

    private void Start()
    {
        FindObjectOfType<DialogueManager>().dialogueInProgress = false;
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    private void Update()
    {
        CheckForInput();
        CheckForDeath();
        CheckForRegeneration(healAmount);
    }

    private void CheckForInput()
    {
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
    }

    private void CheckForRegeneration(int healAmount)
    {
        healTimer -= Time.deltaTime;

        if (!canHeal) return;
        if (currentHealth < maxHealth && healTimer < 0)
        {
            healTimer = healDelay;
            currentHealth += healAmount;
            healthBar.SetHealth(currentHealth);
        }
        else if (currentHealth >= maxHealth && healTimer < 0)
        {
            healTimer = healDelay;
        }
    }

    private void CheckForDeath()
    {
        if (currentHealth < 0)
        {
            dead = true;
            StartCoroutine(Death());
        }
    }

    private IEnumerator Death()
    {
        speed = 0f;
        dead = true;
        animator.SetBool("isDead", true);
        yield return new WaitForSeconds(1f);
        deathEffect.GetComponent<Restart>().Death();
        yield break;
    }

    private void MoveLogic()
    {
        if (!dead && !attacking && Time.timeScale != 0f && !FindObjectOfType<DialogueManager>().dialogueInProgress)
        {
            var speed = 3.5f + stats.speedModifier;
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                speed = 2f + stats.speedModifier;
            }
            else
            {
                speed = 3.5f + stats.speedModifier;
            }
            var movementVector = Vector2.ClampMagnitude(movement, 1);
            var newPosition = rb.position + speed * Time.fixedDeltaTime * movementVector;

            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");

            rb.MovePosition(newPosition);

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

    private void Attack()
    {
        int damage = Random.Range(minDamage, maxDamage) + stats.damageBonus;
        StartCoroutine(Attacking());
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(range.position, attackRange, enemyLayer);
        foreach (Collider2D enemy in hitEnemies)
        {
            damageIndicator.DamageIndication(damage);
            
            if (SceneManager.GetActiveScene().name == "L2-Battle")
            {
                enemy.GetComponent<AttackingWarrior>().TakeDamage(damage);
            }

            if (SceneManager.GetActiveScene().name == "Cove")
            {
                if (enemy.CompareTag("Skeleton"))
                {
                    enemy.GetComponent<SkeletonController>().TakeDamage(damage);
                }
                if (enemy.CompareTag("Crab"))
                {
                    enemy.GetComponent<CrabController>().TakeDamage(damage);
                }
            }

            if (SceneManager.GetActiveScene().name == "L4-Beach" || SceneManager.GetActiveScene().name == "L4-Ship")
            {
                enemy.GetComponent<AttackingPirate>().TakeDamage(damage);
            }
        }
    }

    private IEnumerator Attacking()
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

    private void OnDrawGizmos()
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
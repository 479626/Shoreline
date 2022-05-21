using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AttackingPirateBoss : MonoBehaviour
{
    [Header("Miscellaneous")]
    public bool allowedToAttack;

    [Header("Components")]
    public Animator animator;
    public Transform target;
    public GameObject warrior;
    public PlayerStats stats;

    [Header("Movement")]
    public float defaultSpeed;
    public Rigidbody2D rb;
    private float speed;

    [Header("Combat")]
    public Transform damageRange;
    public float attackRange;
    public float nextAttackTime = 0f;
    public int minDamage, maxDamage;
    public float maxRange, minRange;
    public float attackRate = 1f;
    private bool close = false;

    [Header("Health")]
    public HealthBar healthBar;
    public bool dead;
    public int maxHealth, currentHealth, damage;

    private void Awake()
    {
        allowedToAttack = true;
    }

    private void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    private void CheckHealth()
    {
        if (currentHealth <= 0)
        {
            dead = true;
            StartCoroutine(Die());
        }
    }

    private IEnumerator Die()
    {
        if (SceneManager.GetActiveScene().name == "L4-Beach")
        {
            stats.pirateCrewBossDeath = true;
        }
        else
        {
            stats.defeatedFinalBoss = true;
        }
        animator.SetBool("dead", true);
        yield return new WaitForSeconds(2f);
        if (SceneManager.GetActiveScene().name == "L4-Battle")
        {
            SceneManager.LoadScene(22);
        }
        yield return null;
    }

    private void Update()
    {
        if (!dead)
        {
            CheckHealth();
        }

        if (SceneManager.GetActiveScene().name == "L4-Beach")
        {
            if (warrior.GetComponent<PeacefulWarrior>().dead)
            {
                animator.SetBool("movement", false);
                return;
            }
        }
        else
        {
            if (warrior.GetComponent<AttackingPlayer>().dead)
            {
                animator.SetBool("movement", false);
                return;
            }
        }

        if (Vector3.Distance(target.position, transform.position) <= attackRange)
        {
            if (!(Time.time >= nextAttackTime)) return;
            StartCoroutine(Attacking());
            nextAttackTime = Time.time + 1f / attackRate;
        }

        else if (Vector3.Distance(target.position, transform.position) <= maxRange && Vector3.Distance(target.position, transform.position) >= minRange)
        {
            FindPlayer();
        }
        else
        {
            animator.SetBool("movement", false);
        }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Warrior"))
        {
            close = true;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Warrior"))
        {
            close = false;
        }
    }

    private void FindPlayer()
    {
        if (allowedToAttack && !close)
        {
            animator.SetBool("movement", true);
            animator.SetFloat("X", (target.position.x - transform.position.x));
            animator.SetFloat("Y", (target.position.y - transform.position.y));
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, defaultSpeed * Time.deltaTime);
        }
        else
        {
            animator.SetBool("movement", false);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        SoundManager.instance.AttackSound();

        healthBar.SetHealth(currentHealth);
    }

    IEnumerator Attacking()
    {
        var damage = Random.Range(minDamage, maxDamage);

        defaultSpeed = 0f;
        yield return new WaitForSeconds(1.25f);
        if (Vector3.Distance(target.position, transform.position) <= attackRange && allowedToAttack)
        {
            animator.SetTrigger("attack");
            if (SceneManager.GetActiveScene().name == "L4-Beach")
            {
                warrior.GetComponent<PeacefulWarrior>().TakeDamage(damage);
            }
            else
            {
                warrior.GetComponent<AttackingPlayer>().TakeDamage(damage);
            }
        }
        defaultSpeed = 0.75f;
        yield return null;
    }

    private void OnDrawGizmos()
    {
        if (damageRange == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(damageRange.position, attackRange);
    }
}

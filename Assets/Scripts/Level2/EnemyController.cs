using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Miscellaneous")]
    public bool allowedToAttack;

    [Header("Components")]
    public Animator animator;
    public Transform target;
    public GameObject player;

    [Header("Movement")]
    public float defaultSpeed;
    public Rigidbody2D rb;
    private float speed;

    [Header("Combat")]
    public string enemyName;
    public Transform damageRange;
    public float attackRange;
    public float nextAttackTime = 0f;
    public LayerMask playerLayer;
    public int minDamage, maxDamage;
    public float maxRange, minRange;
    public float attackRate = 1f;

    void Awake()
    {
        allowedToAttack = true;
    }

    void Start()
    {
        target = GameObject.Find("Player").transform;
        player = GameObject.Find("Player");
    }

    void Update()
    {

        if (player.GetComponent<LevelTwoPlayer>().dead == true)
        {
            animator.SetBool("movement", false);
            return;
        }
        if (Vector3.Distance(target.position, transform.position) <= attackRange)
        {
            if (Time.time >= nextAttackTime)
            {
                StartCoroutine(Attacking());
                nextAttackTime = Time.time + 1f / attackRate;
            }
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

    public void FindPlayer()
    {
        if (allowedToAttack)
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

    IEnumerator Attacking()
    {
        int damage = Random.Range(minDamage, maxDamage);

        defaultSpeed = 0f;
        yield return new WaitForSeconds(1.25f);
        if (Vector3.Distance(target.position, transform.position) <= attackRange && allowedToAttack && !player.GetComponent<LevelTwoPlayer>().dead)
        {
            animator.SetTrigger("attack");
            player.GetComponent<LevelTwoPlayer>().TakeDamage(damage);
        }
        defaultSpeed = 0.75f;
        yield break;
    }

    void OnDrawGizmos()
    {
        if (damageRange == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(damageRange.position, attackRange);
    }

}

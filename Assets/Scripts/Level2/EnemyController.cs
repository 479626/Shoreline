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

    [Header("Combat")]
    public Transform damageRange;
    public float attackRange;
    public float nextAttackTime = 0f;
    public int minDamage, maxDamage;
    public float maxRange, minRange;
    public float attackRate = 1f;

    private void Awake()
    {
        allowedToAttack = true;
    }

    private void Start()
    {
        target = GameObject.Find("Player").transform;
        player = GameObject.Find("Player");
    }

    private void Update()
    {

        if (player.GetComponent<AttackingPlayer>().dead == true)
        {
            animator.SetBool("movement", false);
            return;
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

    private void FindPlayer()
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
        var damage = Random.Range(minDamage, maxDamage);

        defaultSpeed = 0f;
        yield return new WaitForSeconds(1.25f);
        if (Vector3.Distance(target.position, transform.position) <= attackRange && allowedToAttack && !player.GetComponent<AttackingPlayer>().dead)
        {
            animator.SetTrigger("attack");
            player.GetComponent<AttackingPlayer>().TakeDamage(damage);
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

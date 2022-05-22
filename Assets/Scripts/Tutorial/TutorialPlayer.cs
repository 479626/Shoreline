using System.Collections;
using UnityEngine;

public class TutorialPlayer : MonoBehaviour
{
    [Header("Movement")]
    public float speed;
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

    private void Update()
    {
        CheckForInput();
    }

    private void CheckForInput()
    {
        if (Input.GetButtonDown("Fire1") && speed != 0)
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
    }

    private void MoveLogic()
    {
        if (!attacking && Time.timeScale != 0f)
        {
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                speed = 2f;
            }
            else
            {
                speed = 3.5f;
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

    }

    private void Attack()
    {
        int damage = Random.Range(minDamage, maxDamage);
        StartCoroutine(Attacking());
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(range.position, attackRange, enemyLayer);
        foreach (Collider2D enemy in hitEnemies)
        {
            damageIndicator.DamageIndication(damage);
            Debug.Log(enemy);
            enemy.GetComponent<TutorialEnemy>().TakeDamage();
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

    public void PlaySound(string id)
    {
        if (id == "walk")
        {
            //SoundManager.instance.WalkSound();
        }
        if (id == "swing")
        {
            //SoundManager.instance.SwingSound();
        }
    }

}

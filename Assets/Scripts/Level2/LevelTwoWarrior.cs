using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelTwoWarrior : MonoBehaviour
{
    public int nextLevelScene, maxHealth, currentHealth, damage;
    public GameObject dialogueManager;
    public PlayerStats stats;
    public HealthBar healthBar;
    public Animator animator;
    public bool dead = false;
    private Rigidbody2D rb;

    void Start()
    {
        dialogueManager.GetComponent<DialogueManager>().finishedDialogue = false;
        rb = GetComponent<Rigidbody2D>();

        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    void Update()
    {
        if (currentHealth < 0)
        {
            dead = true;
            stats.defeatedWarrior = true;
            currentHealth = 200;
            StartCoroutine(Progress());
        }

        if (dialogueManager.GetComponent<DialogueManager>().finishedDialogue)
        {
            gameObject.GetComponent<EnemyController>().allowedToAttack = true;
            dialogueManager.GetComponent<DialogueManager>().finishedDialogue = false;
            SceneManager.LoadScene(nextLevelScene);
        }

    }

    IEnumerator Progress()
    {
        gameObject.GetComponent<EnemyController>().allowedToAttack = false;
        yield return new WaitForSeconds(1f);
        gameObject.GetComponent<DialogueInteraction>().TriggerDialogue(2);
        yield break;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        SoundManager.instance.AttackSound();

        animator.SetTrigger("hurt");
        healthBar.SetHealth(currentHealth);
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

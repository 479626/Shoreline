using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelTwoWarrior : MonoBehaviour
{
    public int nextLevelScene, maxHealth, currentHealth, damage;
    public GameObject dialogueManager, dialogueInteraction;
    public PlayerStats stats;
    public HealthBar healthBar;
    public Animator animator;
    public bool dead = false;
    private Rigidbody2D rb;
    private EnemyController enemyController;

    private void Awake()
    {
        enemyController = gameObject.GetComponent<EnemyController>();
    }

    private void Start()
    {
        dialogueManager.GetComponent<DialogueManager>().finishedDialogue = false;
        rb = GetComponent<Rigidbody2D>();

        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    private void Update()
    {
        CheckHealth();
        CheckForDialogue();
    }

    private IEnumerator Progress()
    {
        enemyController.allowedToAttack = false;
        yield return new WaitForSeconds(1f);
        dialogueInteraction.GetComponent<DialogueInteraction>().TriggerDialogue(2);
        yield break;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        SoundManager.instance.AttackSound();

        animator.SetTrigger("hurt");
        healthBar.SetHealth(currentHealth);
    }

    private void CheckForDialogue()
    {
        if (dialogueManager.GetComponent<DialogueManager>().finishedDialogue)
        {
            enemyController.allowedToAttack = true;
            dialogueManager.GetComponent<DialogueManager>().finishedDialogue = false;
            SceneManager.LoadScene(nextLevelScene);
        }

        if (dialogueManager.GetComponent<DialogueManager>().dialogueInProgress)
        {
            enemyController.allowedToAttack = false;
        }
        else
        {
            enemyController.allowedToAttack = true;
        }
    }

    private void CheckHealth()
    {
        if (currentHealth < 0)
        {
            dead = true;
            stats.defeatedWarrior = true;
            currentHealth = 200;
            StartCoroutine(Progress());
        }
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
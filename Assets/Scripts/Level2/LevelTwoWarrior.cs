using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelTwoWarrior : MonoBehaviour
{
    public int nextLevelScene, maxHealth, currentHealth, damage;
    public GameObject dialogueManager;
    public HealthBar healthBar;
    public Animator animator;
    private bool triggerDialogue, defeated;
    private Rigidbody2D rb;

    void Start()
    {
        triggerDialogue = false;
        defeated = false;
        dialogueManager.GetComponent<DialogueManager>().finishedDialogue = false;
        rb = GetComponent<Rigidbody2D>();

        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    void Update()
    {
        if (currentHealth < 0)
        {
            currentHealth = 200;
            StartCoroutine(Progress());
        }

        if (currentHealth < 0 && dialogueManager.GetComponent<DialogueManager>().finishedDialogue)
        {
            dialogueManager.GetComponent<DialogueManager>().finishedDialogue = false;
            SceneManager.LoadScene(nextLevelScene);
        }
    }

    IEnumerator Progress()
    {
        yield return new WaitForSeconds(1f);
        gameObject.GetComponent<DialogueInteraction>().TriggerDialogue(2);
        yield break;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        animator.SetTrigger("hurt");
        healthBar.SetHealth(currentHealth);
    }
}

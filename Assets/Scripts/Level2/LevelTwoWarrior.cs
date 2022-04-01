using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelTwoWarrior : MonoBehaviour
{

    bool triggerDialogue, defeated;
    public GameObject dialogueManager;
    public int nextLevelScene, maxHealth, currentHealth, damage;
    public HealthBar healthBar;
    public Animator animator;


    void Start()
    {
        triggerDialogue = false;
        defeated = false;
        dialogueManager.GetComponent<DialogueManager>().finishedDialogue = false;

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

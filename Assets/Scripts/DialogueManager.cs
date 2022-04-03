using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    public Text nameText;
    public Text dialogueText;
    public Animator animator;
    public bool finishedDialogue;
    public GameObject player;

    private Queue<string> sentences;

    void Start()
    {
        sentences = new Queue<string>();
        finishedDialogue = false;
    }

    public void StartDialogue(Dialogue dialogue)
    {
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "L2-Battle")
        {
            player.GetComponent<LevelTwoPlayer>().speed = 0f;
        }
        else
        {
            player.GetComponent<PlayerMovement>().speed = 0f;
        }
        animator.SetBool("isOpen", true);

        nameText.text = dialogue.name;
        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();

        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.015F);
            yield return null;
        }
    }

    void EndDialogue()
    {
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "L2-Battle")
        {
            player.GetComponent<LevelTwoPlayer>().speed = 0f;
        }
        else
        {
            player.GetComponent<PlayerMovement>().speed = 3.5f;
        }
        animator.SetBool("isOpen", false);
        finishedDialogue = true;
    }

}

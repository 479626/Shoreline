using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    public Text nameText;
    public Text dialogueText;
    public Image portrait;
    public Animator animator;
    public bool dialogueInProgress;
    public bool finishedDialogue;
    public GameObject player, image;

    private Queue<string> sentences;

    void Start()
    {
        sentences = new Queue<string>();
        finishedDialogue = false;
    }

    public void StartDialogue(Dialogue dialogue)
    {
        dialogueInProgress = true;
        animator.SetBool("isOpen", true);

        if (dialogue.portrait == null)
        {
            image.SetActive(false);
        }
        else
        {
            image.SetActive(true);
            portrait.sprite = dialogue.portrait;
        }
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
        dialogueInProgress = false;
        animator.SetBool("isOpen", false);
        finishedDialogue = true;
    }

}

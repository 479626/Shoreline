using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Button next;
    public Text nameText, dialogueText;
    public Image portrait;
    public Animator animator;
    public bool dialogueInProgress;
    public bool finishedDialogue;
    public GameObject image;

    private Queue<string> sentences;

    private void Start()
    {
        sentences = new Queue<string>();
        finishedDialogue = false;
    }

    public void StartDialogue(Dialogue dialogue)
    {
        dialogueInProgress = true;
        animator.SetBool("isOpen", true);
        next.Select();

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
        SoundManager.instance.MaleSpeechSound();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        SoundManager.instance.PurchaseSound();
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();

        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    private IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.015F);
            yield return null;
        }
    }

    private void EndDialogue()
    {
        EventSystem.current.SetSelectedGameObject(null);
        dialogueInProgress = false;
        animator.SetBool("isOpen", false);
        finishedDialogue = true;
    }
}

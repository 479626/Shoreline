using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        player.GetComponent<PlayerMovement>().speed = 0f;
        animator.SetBool("isOpen", true);

        Debug.Log("Started convo with " + dialogue.name);

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
        Debug.Log("Test message for display next sentence");
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
        // Coroutine to type out sentences, similar to a visual novel
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
        player.GetComponent<PlayerMovement>().speed = 3.5f;
        animator.SetBool("isOpen", false);
        finishedDialogue = true;

        Debug.Log("End of convo");
    }

}

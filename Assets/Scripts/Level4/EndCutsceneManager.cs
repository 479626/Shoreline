using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndCutsceneManager : MonoBehaviour
{
    public GameObject decision;
    public int killUlricScene, killPirateScene;
    public Text elderUlric, captainCrook;
    [SerializeField] private string ulricDialogueTwo, captainDialogueTwo, ulricDialogueThree;
    [SerializeField] private float cutsceneLength, dialogueChangeOne, dialogueChangeTwo, dialogueChangeThree;

    private void Update()
    {
        Timer();
        CheckConditions();
        UpdateDialogue();
    }

    private void Timer()
    {
        cutsceneLength -= Time.deltaTime;
        dialogueChangeOne -= Time.deltaTime;
        dialogueChangeTwo -= Time.deltaTime;
        dialogueChangeThree -= Time.deltaTime;
    }

    public void OnKillUlricButton()
    {
        SceneManager.LoadScene(killUlricScene);
    }

    public void OnKillPirateButton()
    {
        SceneManager.LoadScene(killPirateScene);
    }

    private void UpdateDialogue()
    {
        if (dialogueChangeOne <= 0)
        {
            elderUlric.text = ulricDialogueTwo;
        }

        if (dialogueChangeTwo <= 0)
        {
            captainCrook.text = captainDialogueTwo;
        }

        if (dialogueChangeThree <= 0)
        {
            elderUlric.text = ulricDialogueThree;
        }
    }

    private void CheckConditions()
    {
        if (cutsceneLength <= 0)
        {
            decision.SetActive(true);
        }
    }
}

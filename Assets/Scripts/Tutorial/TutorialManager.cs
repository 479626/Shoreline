using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private TutorialVariables tutorial;
    [SerializeField] private Text instruction;
    private bool finishedExplanation;

    private void Awake()
    {
        StartCoroutine(ExplainScene());
    }

    private void Update()
    {
        if (finishedExplanation)
        {
            CheckTutorialProgress();
        }

        if (tutorial.completedTheTutorial)
        {
            SkipTutorial();
        }
    }

    public void SkipTutorial()
    {
        SceneManager.LoadScene(2);
    }

    private void CheckTutorialProgress()
    {
        if (!tutorial.learnMove)
        {
            instruction.text = "Press W, A, S, or D to move";
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
            {
                tutorial.learnMove = true;
            }
        }

        if (!tutorial.learnSlowWalk && tutorial.learnMove)
        {
            instruction.text = "Hold SHIFT + W, A, S, or D to stroll";
            if ((Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)))
            {
                tutorial.learnSlowWalk = true;
            }
        }

        if (!tutorial.learnQuests && tutorial.learnMove && tutorial.learnSlowWalk)
        {
            instruction.text = "Press Q to check your quests";
            if (Input.GetKeyDown(KeyCode.Q))
            {
                tutorial.learnQuests = true;
            }
        }

        if (tutorial.learnQuests && tutorial.learnQuests && tutorial.learnMove && tutorial.learnSlowWalk)
        {
            instruction.text = "Press LEFT MOUSE BUTTON to attack a training dummy";
        }

        if (tutorial.learnQuests  && tutorial.learnMove && tutorial.learnSlowWalk && tutorial.learnAttack)
        {
            StartCoroutine(CompletedTutorial());
        }
    }

    private IEnumerator CompletedTutorial()
    {
        PlayerPrefs.SetInt("tutorial", 1);
        instruction.text = "Tutorial complete!";
        yield return new WaitForSeconds(3.25f);
        tutorial.completedTheTutorial = true;
        yield return null;
    }

    private IEnumerator ExplainScene()
    {
        var waitTime = new WaitForSeconds(2f);

        instruction.text = "Welcome to Shoreline!";
        yield return waitTime;
        instruction.text = "In this tutorial, you'll learn the basic controls";
        yield return waitTime;
        instruction.text = "Ready?";
        yield return waitTime;
        finishedExplanation = true;
        yield return null;
    }
}

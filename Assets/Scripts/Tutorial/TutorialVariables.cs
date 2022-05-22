using UnityEngine;

[CreateAssetMenu]
public class TutorialVariables : ScriptableObject
{
    public bool learnMove, learnAttack, learnSlowWalk, learnQuests, completedTheTutorial;

    private void OnEnable()
    {
        learnMove = false;
        learnAttack = false;
        learnSlowWalk = false;
        learnQuests = false;    
        completedTheTutorial = false;
    }
}
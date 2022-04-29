using UnityEngine;

[CreateAssetMenu]
public class QuestProgress : ScriptableObject
{
    public int crabKills, discoverBlacksmith, defeatWarrior, meetElderUlric, itemPurchase, levelThreeWarrior;

    private void OnEnable()
    {
        Debug.Log("Resetting all variables in QuestProgress");
        crabKills = 0;
        discoverBlacksmith = 0;
        defeatWarrior = 0;
        meetElderUlric = 0;
        itemPurchase = 0;
        levelThreeWarrior = 0;
    }
}

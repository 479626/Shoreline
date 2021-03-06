using UnityEngine;

[CreateAssetMenu]
public class PlayerStats : ScriptableObject
{
    public float speedModifier;
    public int coins, damageBonus, crabKills, pirateKills, upgradesPurchased;
    public int currentLevel;
    public string swordType, bootType;
    public bool greedy, discoverBlacksmith, defeatedWarrior, seenLevelOneCutscene, pirateCrewBossDeath, defeatedFinalBoss, defeatedGateKeeper;

    private void OnEnable()
    {
        currentLevel = 0;
        seenLevelOneCutscene = false;

        discoverBlacksmith = false;
        defeatedWarrior = false;
        greedy = false;
        pirateCrewBossDeath = false;
        defeatedFinalBoss = false;
        defeatedGateKeeper = false;

        pirateKills = 0;
        upgradesPurchased = 0;
        crabKills = 0;
        coins = 0;
        greedy = false;
        discoverBlacksmith = false;
        defeatedWarrior = false;
        speedModifier = 0;
        damageBonus = 0;
        bootType = "Old Running Shoes";
        swordType = "Rusty Cutlass";
    }
}

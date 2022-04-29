using UnityEngine;

[CreateAssetMenu]
public class PlayerStats : ScriptableObject
{
    public float speedModifier;
    public int coins, damageBonus;
    public int currentLevel;
    public string swordType, bootType;
    public bool greedy, discoverBlacksmith, defeatedWarrior;

    private void OnEnable()
    {
        Debug.Log("Resetting all variables in PlayerStats");
        currentLevel = 0;

        discoverBlacksmith = false;
        defeatedWarrior = false;
        greedy = false;

        coins = 0;
        greedy = false;
        discoverBlacksmith = false;
        defeatedWarrior = false;
        speedModifier = 0;
        damageBonus = 0;
        bootType = "Old Running Shoes";
        swordType = "Rusty Rapier";
    }
}
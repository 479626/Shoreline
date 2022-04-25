using UnityEngine;

[CreateAssetMenu]
public class PlayerStats : ScriptableObject
{
    public float speedModifier;
    public int coins, damageBonus;
    public int currentLevel;
    public string swordType, bootType;
    public bool greedy, discoverBlacksmith, defeatedWarrior;
}

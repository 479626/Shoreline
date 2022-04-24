using UnityEngine;

[CreateAssetMenu]
public class PlayerStats : ScriptableObject
{
    public float speedModifier;
    public int damageBonus;
    public string swordType, bootType;
    public bool greedy;
}

using UnityEngine;

[CreateAssetMenu]
public class InteractionCounter : ScriptableObject
{
    public int levelOne, levelThree, levelThreeWarrior;
    public int npcBeth, npcPete, npcPeteJr, npcMary, npcAnne, npcChristopher, npcTimmy, npcGary, npcUlric;

    private void OnEnable()
    {
        levelThreeWarrior = 0;
        levelOne = 0;
        levelThree = 0;
        npcBeth = 0;
        npcPete = 0;
        npcPeteJr = 0;
        npcMary = 0;
        npcAnne = 0;
        npcChristopher = 0;
        npcTimmy = 0;
        npcGary = 0;
        npcUlric = 0;
    }
}

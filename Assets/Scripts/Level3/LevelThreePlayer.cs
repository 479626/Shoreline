using UnityEngine;

public class LevelThreePlayer : MonoBehaviour
{
    public PlayerStats stats;

    void Awake()
    {
        stats.currentLevel = 3;       
    }
}

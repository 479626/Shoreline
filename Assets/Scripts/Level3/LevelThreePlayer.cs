using UnityEngine;

public class LevelThreePlayer : MonoBehaviour
{
    public PlayerStats stats;

    private void Awake()
    {
        stats.currentLevel = 3;       
    }
}

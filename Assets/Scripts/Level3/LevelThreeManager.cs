using UnityEngine;

public class LevelThreeManager : MonoBehaviour
{
    public PlayerStats stats;

    private void Awake()
    {
        stats.currentLevel = 3;       
    }
}

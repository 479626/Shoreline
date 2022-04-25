using UnityEngine;

public class LevelThreePlayer : MonoBehaviour
{
    public PlayerStats stats;

    void Awake()
    {
        Debug.Log("Recognised Level 3 and saved progress");
        stats.currentLevel = 3;       
    }
}

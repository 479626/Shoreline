using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelThreePlayer : MonoBehaviour
{
    
    void Awake()
    {

    }

    public void PlaySound(string id)
    {
        if (id == "walk")
        {
            SoundManager.instance.WalkSound();
        }
    }

}

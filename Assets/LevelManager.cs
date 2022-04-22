using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public Animator anim;
    public static LevelManager instance;

    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    public IEnumerator LoadLevel(int buildIndex)
    {
        yield return new WaitForSeconds(0.1f);
        anim.SetTrigger("Load");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(buildIndex);
        yield break;
    }
}

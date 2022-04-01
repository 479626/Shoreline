using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    public Animator animator;

    void Start()
    {
        animator.SetBool("on", false);
    }

    public void Death()
    {
        animator.SetBool("on", true);
    }

    public void OnButtonClick()
    {
        animator.SetBool("on", false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

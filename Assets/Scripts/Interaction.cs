using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    public Animator animator;

    public void InteractOn()
    {
        animator.SetBool("isOn", true);
    }

    public void InteractOff()
    {
        animator.SetBool("isOn", false);
    }
}

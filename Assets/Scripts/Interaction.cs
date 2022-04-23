using UnityEngine;

public class Interaction : MonoBehaviour
{
    public Animator animator;

    void Start()
    {
        animator.SetBool("isOn", false);
    }

    public void InteractOn()
    {
        Debug.Log("Found player");
        animator.SetBool("isOn", true);
    }

    public void InteractOff()
    {
        Debug.Log("Lost player");
        animator.SetBool("isOn", false);
    }
}

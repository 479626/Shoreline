using UnityEngine;

public class Interaction : MonoBehaviour
{
    public Animator animator;

    private void Start()
    {
        animator.SetBool("isOn", false);
    }

    public void InteractOn()
    {
        animator.SetBool("isOn", true);
    }

    public void InteractOff()
    {
        animator.SetBool("isOn", false);
    }
}

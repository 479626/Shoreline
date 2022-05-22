using UnityEngine;

public class TutorialEnemy : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private TutorialVariables tutorial;

    public void TakeDamage()
    {
        if (!tutorial.learnAttack)
        {
            tutorial.learnAttack = true;
        }
        anim.SetTrigger("spin");
    }
}

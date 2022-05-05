using UnityEngine;
using UnityEngine.UI;

public class DamageIndicator : MonoBehaviour
{
    public Animator damageIndicator;
    public Text damageAmount;

    public void DamageIndication(int damage)
    {
        damageAmount.text = damage.ToString();
        damageIndicator.SetTrigger("Open");
    }
}

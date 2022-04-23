using UnityEngine;
using UnityEngine.UI;


public class HealthBar : MonoBehaviour
{
    public Slider healthBar;
    public Gradient gradient;
    public Image fill;
    
    public void SetMaxHealth(int health)
    {
        Debug.Log("A script has accessed 'SetMaxHealth' " + health);
        healthBar.maxValue = health;
        healthBar.value = health;

        fill.color = gradient.Evaluate(1f);
    }

    public void SetHealth(int health)
    {
        Debug.Log("A script has accessed 'SetHealth' " + health);
        healthBar.value = health;

        fill.color = gradient.Evaluate(healthBar.normalizedValue);
    }
}

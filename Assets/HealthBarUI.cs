using UnityEngine;
using UnityEngine.UI;
public class HealthBarUI : MonoBehaviour
{
    private float Health, MaxHealth;
    [SerializeField]
    private Slider healthBar;

    public void SetMaxHealth(float maxHealth)
    {
        MaxHealth = maxHealth;
    }

    public void SetHealth(float health)
    {
        Health = health;
        healthBar.value = Health / MaxHealth;
    }
}

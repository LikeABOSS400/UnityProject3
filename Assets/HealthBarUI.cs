using UnityEngine;
using UnityEngine.UI;
public class HealthBarUI : MonoBehaviour
{
    private float Health, MaxHealth = 100f;
    [SerializeField]
    private Slider healthBar;


    void Start()
    {
        Health = MaxHealth;        
    }

    void Update()
    {
        if (healthBar.value != Health)
        {
            healthBar.value = Health;
        }   
    }

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

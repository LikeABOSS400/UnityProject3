using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private HealthBarUI healthBar;
    private Damage damage;
    public float health;
    public float maxHealth;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        health = Mathf.Clamp(health, 0, maxHealth);

        healthBar.SetHealth(health);
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }
}

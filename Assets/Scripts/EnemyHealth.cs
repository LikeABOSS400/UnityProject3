using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float health;
    public float maxHealth;
    private bool IsDead = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = maxHealth;
        UnitsManager.Instance.CountEnemy();
    }


    public void TakeDamage(float amount)
    {
        if (IsDead) return;

        health -= amount;

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        IsDead = true;
        UnitsManager.Instance.DefeatEnemy();
        Destroy(gameObject);
    }

    public void SetHealth(float Health)
    {
        health = Health;
    }
}

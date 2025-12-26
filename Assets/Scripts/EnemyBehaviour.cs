using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class EnemyBehaviour : MonoBehaviour
{
    public int MaxHealth = 100;
    int currentHealth;
    private Transform target;
    public float speed = 2f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = MaxHealth;
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        // tutaj animacja do otrzymywania obrażeń
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Enemy died!");
        Destroy(gameObject);
        // tutaj animacja umierania
    }
    // Update is called once per frame
    void Update()
    {
        if (target == null) return;

        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }
}

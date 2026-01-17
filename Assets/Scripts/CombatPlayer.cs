using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CombatPlayer : MonoBehaviour
{
    public Animator animator;

    public Transform AttackPoint;
    public float AttackRange = 1f;
    public LayerMask enemyLayers;

    public EnemyHealth enemyHealth;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Attack();
        }
        
    }
    void Attack()
    {
        animator.SetTrigger("Attack");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(AttackPoint.position, AttackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("We hit " + enemy.name);
            EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(25);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        if(AttackPoint == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(AttackPoint.position, AttackRange);        
    }
}

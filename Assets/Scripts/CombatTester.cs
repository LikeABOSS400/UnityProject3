// Author: Nick Hwang
// For: Beat Em Up Style Tutorials
// youtube.com/c/nickhwang

using System.Collections.Generic;
using UnityEngine;

public class CombatTester : MonoBehaviour
{
    [SerializeField] private bool canAttack = true;
    
    [SerializeField] private Collider2D inLineCollider;
    
    [SerializeField] private LayerMask enemyLayer;
    
    PlayerInput input;
    Controls controls = new Controls();
    private ContactFilter2D contactFilter2D;
    public List<Collider2D> cols = new List<Collider2D>();
    
    private void Awake()
    {
        input = GetComponent<PlayerInput>();

        contactFilter2D = new ContactFilter2D();
        contactFilter2D.useTriggers = true;
        contactFilter2D.SetLayerMask(enemyLayer);
    }

    // Update is called once per frame
    void Update()
    {
        if (!canAttack) return;

        controls = input.GetInput();
        if (controls.AttackState)
        {
            cols.Clear();

            int hits = inLineCollider.Overlap(contactFilter2D, cols);

            if (hits > 0)
            {
                foreach (var col in cols)
                {
                    print(col.transform.name);
                    if (col.TryGetComponent(out EnemyHealth enemyHealth))
                    {
                        enemyHealth.TakeDamage(25);
                        Debug.Log("We hit: " + col.name);
                    }

                }
            }
        }
    }

}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
public class EnemyBehaviour : MonoBehaviour
{
    private Transform target;
    private Health playerHealth;
    private EnemyHealth enemyHealth;

    public float speed = 2f;
    public float range = 1.5f;
    public float coolDown = 1f;
    public float backOffChance = 0.2f;
    public float BackoffDistance = 2f;
    public float BackOffTimer = 1.2f;

    private float lastAttackTime = 0f;
    public bool BackingOff = false;
    public float backOffEndTime;
    private Vector3 backOffTarget;
    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if(player != null)
        {
            target = player.transform;
            playerHealth = player.GetComponent<Health>();
        }

        enemyHealth = GetComponent<EnemyHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null) return;

        float distance = Vector2.Distance(transform.position, target.position);

        if(BackingOff)
        {
            transform.position = Vector2.MoveTowards(transform.position, backOffTarget, speed * Time.deltaTime);

            if (Time.time >= backOffEndTime)
                BackingOff = false;

            return;
        }

        if(distance > range)
        {
            MoveTowards(target.position);
            return;
        }

        if(Time.time > lastAttackTime + coolDown)
        {
            Attack(5f);

            if (Random.value < backOffChance)
                StartBackOff();
        }

    }

    void MoveTowards(Vector3 target)
    {
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
    }

    void Attack(float damage)
    {
        lastAttackTime = Time.time;
        playerHealth.TakeDamage(damage);
        Debug.Log("Enemy attack!");
    }

    void StartBackOff()
    {
        BackingOff = true;
        backOffEndTime = Time.time + BackOffTimer;

        Vector3 direction = (transform.position - target.position).normalized;
        backOffTarget = transform.position + direction * BackoffDistance;
    }

}

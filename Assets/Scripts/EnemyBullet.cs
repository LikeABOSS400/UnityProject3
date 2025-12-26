using System;
using System.Security.Cryptography;
using UnityEngine;
public class EnemyBullet:MonoBehaviour
{
	public float speed = 5f;
	public int damage = 10;
	private Transform player;
	private Rigidbody2D rb;
	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		var playerObj = GameObject.FindGameObjectWithTag("Player");

		if(player != null)
        {
			player = playerObj.transform;
			Vector2 direction = (player.position - transform.position).normalized;
			rb.velocity = direction * speed;
        }

		Destroy(gameObject, 5f);
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
			Debug.Log("Shoot");
			Destroy(gameObject);
        }
    }
}

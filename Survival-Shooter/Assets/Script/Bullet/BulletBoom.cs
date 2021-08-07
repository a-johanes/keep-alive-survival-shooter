using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBoom : MonoBehaviour
{

    public float speed = 10f;
    public int damage = 40;
    public float radius = 10f;
    public GameObject explosionEffect;
    public Rigidbody2D rigidbody2D;

    void Update()
    {
        rigidbody2D.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius);
            foreach (Collider2D nearbyObject in colliders)
            {
                if (nearbyObject.CompareTag("Enemy"))
                {
                    EnemyHealth enemy = nearbyObject.GetComponent<EnemyHealth>();
                    if (enemy != null)
                    {
                        enemy.TakeDamage(damage);
                    }
                }
            }

            Instantiate(explosionEffect, transform.position, transform.rotation);   
            gameObject.SetActive(false);
        }

    }
}

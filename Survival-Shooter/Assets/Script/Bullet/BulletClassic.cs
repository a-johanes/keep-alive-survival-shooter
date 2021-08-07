using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletClassic : MonoBehaviour
{

    public float speed = 20f;
    public int damage = 40;
    public Rigidbody2D rigidbody2D;

    void Update()
    {
        rigidbody2D.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
        {
            EnemyHealth enemy;
            enemy = other.GetComponent<EnemyHealth>();

            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
            gameObject.SetActive(false);
        }

    }
}

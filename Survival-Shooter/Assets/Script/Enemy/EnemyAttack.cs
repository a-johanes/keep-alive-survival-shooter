using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour
{
    public float timeBetweenAttacks = 0.5f;
    public int attackDamage = 10;

    GameObject m_Player;
    PlayerHealth m_PlayerHealth;
    EnemyHealth m_EnemyHealth;
    bool m_PlayerInRange;
    float m_Timer;


    void Awake ()
    {
        m_Player = GameObject.FindGameObjectWithTag ("Player");
        m_PlayerHealth = m_Player.GetComponent <PlayerHealth> ();
        m_EnemyHealth = GetComponent<EnemyHealth>();
    }


    void OnTriggerEnter2D (Collider2D other)
    {
        if(other.gameObject == m_Player)
        {
            m_PlayerInRange = true;
        }
    }


    void OnTriggerExit2D (Collider2D other)
    {
        if(other.gameObject == m_Player)
        {
            m_PlayerInRange = false;
        }
    }


    void Update ()
    {
        m_Timer += Time.deltaTime;

        if(m_Timer >= timeBetweenAttacks && m_PlayerInRange && m_EnemyHealth.currentHealth > 0)
        {
            Attack ();
        }
    }


    void Attack ()
    {
        m_Timer = 0f;

        if(m_PlayerHealth.currentHealth > 0)
        {
            m_PlayerHealth.TakeDamage (attackDamage);
        }
    }
}
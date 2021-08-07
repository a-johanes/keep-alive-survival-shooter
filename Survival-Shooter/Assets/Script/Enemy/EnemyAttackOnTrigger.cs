using UnityEngine;
using System.Collections;

public class EnemyAttackOnTrigger : MonoBehaviour
{
    public float timeBetweenAttacks = 0.5f;
    public int attackDamage = 10;

    GameObject m_Player;
    PlayerHealth m_PlayerHealth;
    float m_Timer;


    void Awake ()
    {
        m_Player = GameObject.FindGameObjectWithTag ("Player");
        m_PlayerHealth = m_Player.GetComponent <PlayerHealth> ();
    }


    void OnTriggerStay2D (Collider2D other)
    {
        if(other.gameObject == m_Player && m_Timer >= timeBetweenAttacks)
        {
            Attack();
        }
    }
    


    void Update ()
    {
        m_Timer += Time.deltaTime;
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
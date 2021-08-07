using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyMovement : MonoBehaviour
{
    public AIPath aiPath;

    Transform m_Player;
    IAstarAI m_Ai;
    PlayerHealth m_PlayerHealth;

    void OnEnable () {
        m_Ai = GetComponent<IAstarAI>();
        if (m_Ai != null) m_Ai.onSearchPath += Update;
    }
    
    void OnDisable () {
        if (m_Ai != null) m_Ai.onSearchPath -= Update;
    }

    void Start()
     {
         m_Player = GameObject.FindGameObjectWithTag("Player").transform;
         m_PlayerHealth = m_Player.GetComponent<PlayerHealth>();

     }

    void Update ()
    {
        if (m_Player != null && m_Ai != null) m_Ai.destination = m_Player.position;

        if (aiPath.velocity.x >= 0.01f)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        } else if (aiPath.velocity.x <= -0.01f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        if (m_PlayerHealth.currentHealth <= 0f)
            aiPath.enabled = false;
    }
}

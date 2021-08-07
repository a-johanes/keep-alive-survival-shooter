using System;
using Pathfinding;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyManager : MonoBehaviour
{
    public PlayerHealth playerHealth;
    // public GameObject enemy;
    public string enemyPoolTag;
    public float spawnTime = 3f;
    public float deltaSpawnTime = 0.01f;
    public float minSpawnTime = 0.01f;
    public int healthModifier = 20;
    public int scoreModifier = 5;
    public Transform[] spawnPoints;

    private int m_Spawn = 0;
    private float m_Timer = 0;

    void Update ()
    {
        m_Timer += Time.deltaTime;

        if(m_Timer >= spawnTime && playerHealth.currentHealth > 0f)
        {
            Spawn();
        }
    }


    void Spawn ()
    {
        m_Timer = 0f;
        
        int spawnPointIndex = Random.Range (0, spawnPoints.Length);

        GameObject newEnemy = ObjectPooling.instance.SpawnFromPool(enemyPoolTag, spawnPoints[spawnPointIndex].position,
            spawnPoints[spawnPointIndex].rotation);
        // GameObject newEnemy =  Instantiate (enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
        if (newEnemy != null)
        {
            EnemyHealth enemyHealth = newEnemy.GetComponent<EnemyHealth>();
            enemyHealth.startingHealth += m_Spawn * healthModifier;
            enemyHealth.scoreValue += (int) Math.Round((double) m_Spawn / 20) * healthModifier; 
            AIPath aiPath = newEnemy.GetComponent<AIPath>();
            aiPath.maxSpeed += m_Spawn * deltaSpawnTime;
            
        }

        m_Spawn++;
        if (spawnTime > minSpawnTime)
            spawnTime -= deltaSpawnTime;
    }
}

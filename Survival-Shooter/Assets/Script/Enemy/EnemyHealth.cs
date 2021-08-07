using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public int scoreValue = 10;
    public float deathTime = 0.3f;
    public AudioSource hitAudioSource;
    public AudioSource deathAudioSource;
    
    CircleCollider2D m_CircleCollider2D;
    bool m_IsDead;


    void Awake ()
    {
        m_CircleCollider2D = GetComponent<CircleCollider2D>();

        currentHealth = startingHealth;
    }
    


    public void TakeDamage (int amount)
    {
        if(m_IsDead)
            return;

        hitAudioSource.Play ();

        currentHealth -= amount;
            
        if(currentHealth <= 0)
        {
            Death ();
        }
    }


    void Death ()
    {
        m_IsDead = true;

        m_CircleCollider2D.isTrigger = true;

        deathAudioSource.pitch = 3;
        deathAudioSource.Play();

        GetComponent <Rigidbody2D> ().isKinematic = true;
        ScoreManager.score += scoreValue;
        Invoke(nameof(Deactivate), deathTime);
    }

    void Deactivate()
    {
        currentHealth = startingHealth;
        m_IsDead = false;
        gameObject.SetActive(false);
    }
}
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;


public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public Animator healthAnimator;
    public Image damageImage;
    public AudioSource hitAudioSource;
    public AudioSource playerDeathAudio;
    public float flashSpeed = 5f;
    public Color hitColor = new Color(1f, 0f, 0f, 0.1f);


    Animator m_Animator;
    PlayerMovement m_PlayerMovement;
    PlayerShoot m_PlayerShoot;
    private AudioClip m_HitClip;
    private CharacterController2D m_PlayerController;
    bool m_IsDead;
    bool m_Damaged;
    private static readonly int Dead = Animator.StringToHash("Dead");
    private static readonly int HealthIndex = Animator.StringToHash("HealthIndex");


    void Awake ()
    {
        m_Animator = GetComponent <Animator> ();
        // playerHitAudio = GetComponent <AudioSource> ();
        m_PlayerMovement = GetComponent <PlayerMovement> ();
        m_PlayerShoot = GetComponentInChildren <PlayerShoot> ();
        m_PlayerController = GetComponent<CharacterController2D>();
        currentHealth = startingHealth;
    }


    void Update ()
    {
        if(m_Damaged)
        {
            damageImage.color = hitColor;
        }
        else
        {
            damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        m_Damaged = false;
    }


    public void TakeDamage (int amount)
    {
        m_Damaged = true;

        currentHealth -= amount;

        AnimateHealth();

        hitAudioSource.Play ();

        if(currentHealth <= 0 && !m_IsDead)
        {
            Death ();
        }
    }

    private void AnimateHealth()
    {
        int healthLabel = 0;
        if (currentHealth > 80)
            healthLabel = 100;
        else if (currentHealth > 60)
            healthLabel = 80;
        else if (currentHealth > 40)
            healthLabel = 60;
        else if (currentHealth > 30)
            healthLabel = 40;
        else if (currentHealth > 20)
            healthLabel = 20;
        healthAnimator.SetInteger(HealthIndex, healthLabel);

    }


    void Death ()
    {
        m_IsDead = true;
        
        m_Animator.SetTrigger (Dead);

        playerDeathAudio.Play();

        m_PlayerMovement.enabled = false;
        m_PlayerShoot.enabled = false;
        m_PlayerController.enabled = false;

        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
    }


    // public void RestartLevel ()
    // {
    //     SceneManager.LoadScene (0);
    // }
}
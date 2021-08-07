using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerShoot : MonoBehaviour
{
    public CharacterController2D controller;
    public float timeBetweenClassicBullets = 0.15f;
    public float timeBetweenBoomBullets = 1.0f;
    public Transform firePoint;
    public string classicBulletPoolTag;
    public string boomBulletPoolTag;
    
    
    float m_ClassicTimer;
    float m_BoomTimer;
    Animator m_Animator;
    AudioSource m_GunAudio;
    private static readonly int IsShooting = Animator.StringToHash("IsShooting");

    private void Start()
    {
        m_Animator = GetComponentInParent<Animator>();
        m_GunAudio = GetComponent<AudioSource> ();
        
    }

    void Update()
    {
        m_ClassicTimer += Time.deltaTime;
        m_BoomTimer += Time.deltaTime;

        if (Input.GetButton("Fire2"))
        {
            if (m_BoomTimer >= timeBetweenBoomBullets && Time.timeScale != 0f)
            {
                Shoot(boomBulletPoolTag);
                m_BoomTimer = 0f;
            }
        }
        else if (Input.GetButton("Fire1"))
        {
            if (m_ClassicTimer >= timeBetweenClassicBullets && Time.timeScale != 0f)
            {
                Shoot(classicBulletPoolTag);
                m_ClassicTimer = 0f;
            }
        }
        
    }

    void Shoot (string bulletPoolTag)
    {

        m_GunAudio.Play ();

        ObjectPooling.instance.SpawnFromPool(bulletPoolTag, firePoint.position, firePoint.rotation);
        // Instantiate(bullet, firePoint.position, firePoint.rotation);

        m_Animator.SetTrigger(IsShooting);
    }
}

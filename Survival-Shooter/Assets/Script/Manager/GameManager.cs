using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public PlayerHealth playerHealth;
    // public float restartDelay = 5f;


    Animator anim;
    // float m_RestartTimer;


    void Awake()
    {
        anim = GetComponent<Animator>();
    }


    void Update()
    {
        if (playerHealth.currentHealth <= 0)
        {
            anim.SetTrigger("GameOver");

            // m_RestartTimer += Time.deltaTime;
            //
            // if (m_RestartTimer >= restartDelay)
            // {
            //     SceneManager.LoadScene("Scenes/Main");
            //     Application.LoadLevel(Application.loadedLevel);
            // }
        }
    }

    public void BackToMain()
    {
        SceneManager.LoadScene("Scenes/MainMenu");
    }

    public void Restart()
    {
        SceneManager.LoadScene("Scenes/Main");
    }
}
using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public float runSpeed = 40f;

    private float m_HorizontalMove = 0f;
    private bool m_IsJump = false;

    void Update ()
    {
        m_HorizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        if (Input.GetButton("Jump"))
        {
            m_IsJump = true;
        }
    }

    void FixedUpdate()
    {
        controller.Move(m_HorizontalMove * Time.fixedDeltaTime, m_IsJump);
        m_IsJump = false;
    }
}

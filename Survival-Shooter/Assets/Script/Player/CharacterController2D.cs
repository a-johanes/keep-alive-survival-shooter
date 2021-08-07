using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class CharacterController2D : MonoBehaviour
{
    [SerializeField] private float jumpForce = 400f; // Amount of force added when the player jumps.
    [Range(0, .3f)] [SerializeField] private float movementSmoothing = .05f; // How much to smooth out the movement
    [SerializeField] private bool airControl = false; // Whether or not a player can steer while jumping;
    [SerializeField] private LayerMask whatIsGround; // A mask determining what is ground to the character
    [SerializeField] private Transform groundCheck; // A position marking where to check if the player is grounded.
    [SerializeField] private AudioSource jumpAudioSource; // AudioSource to play when jump
    [SerializeField] private AudioSource landAudioSource; // AudioSource to play when land


    Animator m_Animator; // Reference to the animator component.

    // AudioSource m_ControlAudio;
    private AudioClip m_HitClip;
    const float KGroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
    private bool m_Grounded; // Whether or not the player is grounded.
    private bool m_Walking; // Whether or not the player is walking.
    private Rigidbody2D m_Rigidbody2D;
    private bool m_FacingRight = true; // For determining which way the player is currently facing.
    private Vector3 m_Velocity = Vector3.zero;
    private static readonly int IsWalking = Animator.StringToHash("IsWalking");
    private static readonly int IsJumping = Animator.StringToHash("IsJumping");

    [System.Serializable]
    public class BoolEvent : UnityEvent<bool>
    {
    }

    private void Awake()
    {
        m_Animator = GetComponent<Animator>();
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        m_Animator = GetComponent<Animator>();
        // m_ControlAudio = new AudioSource();
    }

    private void FixedUpdate()
    {
        bool wasGrounded = m_Grounded;
        m_Grounded = false;

        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        // This can be done using layers instead but Sample Assets will not overwrite your project settings.
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, KGroundedRadius, whatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                m_Grounded = true;
                if (wasGrounded == false)
                {
                    landAudioSource.Play();
                }
            }
        }

        Animating();
    }


    public void Move(float move, bool jump)
    {
        //only control the player if grounded or airControl is turned on
        if (m_Grounded || airControl)
        {
            m_Walking = move != 0f;
            // Move the character by finding the target velocity
            Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
            // And then smoothing it out and applying it to the character
            m_Rigidbody2D.velocity =
                Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, movementSmoothing);

            // If the input is moving the player right and the player is facing left...
            if (move > 0 && !m_FacingRight)
            {
                // ... flip the player.
                Flip();
            }
            // Otherwise if the input is moving the player left and the player is facing right...
            else if (move < 0 && m_FacingRight)
            {
                // ... flip the player.
                Flip();
            }
        }

        // If the player should jump...
        if (m_Grounded && jump)
        {
            // Add a vertical force to the player.
            m_Grounded = false;
            m_Rigidbody2D.AddForce(new Vector2(0f, jumpForce));
            jumpAudioSource.Play();
        }
    }


    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        // Rotate the player
        transform.Rotate(0f, 180f, 0f);
    }

    void Animating()
    {
        // Tell the animator
        m_Animator.SetBool(IsWalking, m_Walking);
        m_Animator.SetBool(IsJumping, !m_Grounded);
    }
}
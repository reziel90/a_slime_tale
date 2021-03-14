using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerManager : MonoBehaviour
{
    private Rigidbody2D _rigidBody2D;
    private Vector3 m_Velocity = Vector3.zero;
    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;  // How much to smooth out the movement
    [Range(0, 100f)] [SerializeField] private float m_Speed = 50f;
    [Range(0, 100f)] [SerializeField] private float m_Jump = 50f;
    [SerializeField] bool inAirMovement = false;
    [SerializeField] bool enableDoubleJump = false;
    private bool isGrounded;
    private bool isDoubleJump;
    private int groundMask;

    void Start()
    {
        _rigidBody2D = GetComponent<Rigidbody2D>();
        groundMask = LayerMask.GetMask("Ground");
    }

    public void Jump()
    {
        if (isGrounded || (enableDoubleJump && !isDoubleJump))
        {
            if (!isGrounded)
            {
                isDoubleJump = true;
            }
            Vector2 _forceJump = new Vector2(_rigidBody2D.velocity.x, m_Jump * 10f);
            _rigidBody2D.AddForce(_forceJump);

        }
    }

    public void Move(float horizontalMagnitude)
    {
        if (inAirMovement || isGrounded)
        {
            float horizontalVelocity = horizontalMagnitude * m_Speed * 10f;
            Vector3 targetVelocity = new Vector2(horizontalVelocity, _rigidBody2D.velocity.y);
            _rigidBody2D.velocity = Vector3.SmoothDamp(_rigidBody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
        }
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, 0.52f, groundMask).collider != null ? true : false;
        isDoubleJump = isGrounded ? false : isDoubleJump;
    }
}

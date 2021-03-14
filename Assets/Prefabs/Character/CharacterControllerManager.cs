using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerManager : MonoBehaviour
{

    Rigidbody2D m_rigidBody2D;
    private Vector3 m_Velocity = Vector3.zero;
    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;  // How much to smooth out the movement
    [Range(0, 100f)] [SerializeField] private float m_Speed = 40f;

    void Start()
    {
        m_rigidBody2D = GetComponent<Rigidbody2D>();
    }

    public void Jump()
    {

    }

    public void Crouch()
    {


    }

    public void Move(float horizontalMagnitude)
    {
        float horizontalVelocity = horizontalMagnitude * m_Speed;
        Vector3 targetVelocity = new Vector2(horizontalVelocity*10f, m_rigidBody2D.velocity.y);
        m_rigidBody2D.velocity = Vector3.SmoothDamp(m_rigidBody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
    }
}

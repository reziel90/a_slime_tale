using System;
using UnityEngine;
using static PlayerController;

public class PlayerActions : MonoBehaviour, IGameplayActions
{
    private Rigidbody2D _rigidBody2D;
    private Animator _animator;
    [Range(0, 100f)] [SerializeField] private float m_Speed = 10f;
    [Range(0, 100f)] [SerializeField] private float m_Jump = 30f;
    [SerializeField] bool inAirMovement = false;
    [SerializeField] bool enableDoubleJump = false;
    private bool _isGrounded;
    private bool _isDoubleJump;
    private bool _isFalling;
    private int _groundMask;
    private PlayerController _controller;
    [SerializeField] private float _velocity_X;

    public void Start()
    {
        _rigidBody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();

        _groundMask = LayerMask.GetMask("Ground");
    }
    public void Awake()
    {
        _controller = new PlayerController();
        _controller.Gameplay.SetCallbacks(this);
    }

    public void OnEnable()
    {
        _controller.Enable();
    }
    public void OnDisable()
    {
        _controller.Disable();
    }

    public void Update()
    {
        _isGrounded = Physics2D.Raycast(transform.position, Vector2.down, 0.52f, _groundMask).collider != null ? true : false;
        _isDoubleJump = _isGrounded ? false : _isDoubleJump;
        _isFalling = _rigidBody2D.velocity.y < 0 && !_isGrounded;
    }

    public void OnHide(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if (context.started)
        {
        }
        else if (context.performed)
        {
        }
        else if (context.canceled)
        {
        }

        throw new NotImplementedException();
    }

    public void OnJump(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if (context.started)
        {

        }
        else if (context.performed)
        {
            if (_isGrounded || (enableDoubleJump && !_isDoubleJump))
            {
                if (!_isGrounded)
                {
                    _isDoubleJump = true;
                }
                Vector2 _forceJump = new Vector2(_rigidBody2D.velocity.x, m_Jump * 10f);
                _rigidBody2D.AddForce(_forceJump);
            }
        }
        else if (context.canceled)
        {

        }
    }

    public void OnMove(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if (inAirMovement || _isGrounded)
        {
            _velocity_X = context.ReadValue<float>() * m_Speed * 10f;
        }
    }

    void FixedUpdate()
    {
        _rigidBody2D.velocity = new Vector2(_velocity_X * Time.fixedDeltaTime, _rigidBody2D.velocity.y);
    }
}

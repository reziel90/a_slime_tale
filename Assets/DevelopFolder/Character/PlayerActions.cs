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
    [SerializeField] bool enableRotateOnWalls = false;
    private bool _isGrounded;
    private bool _isHangedLeft, _isHangedRight;
    private bool _isDoubleJump;
    private bool _isFalling;
    private int _groundMask;
    private PlayerController _controller;
    private float _velocity_X;
    [Range(0, 5)] [SerializeField] private float _groundDistance = 0.4f;
    [Range(0, 5)] [SerializeField] private float _wallDistance = 0.5f;

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
        _controller?.Enable();
    }
    public void OnDisable()
    {
        _controller?.Disable();
    }

    private void CheckGround()
    {
        _isGrounded = Physics2D.Raycast(transform.position, Vector2.down, _groundDistance, _groundMask).collider != null ? true : false;
    }

    private void CheckDoubleJump()
    {
        _isDoubleJump = _isGrounded ? false : _isDoubleJump;
    }

    private void CheckHanged()
    {
        _isHangedLeft = Physics2D.Raycast(transform.position, Vector2.left, _wallDistance, _groundMask).collider != null ? true : false;
        _isHangedRight = Physics2D.Raycast(transform.position, Vector2.right, _wallDistance, _groundMask).collider != null ? true : false;
        if (enableRotateOnWalls) RotateHanged();
    }

    private void RotateHanged()
    {
        if (_isHangedLeft && !_isGrounded)
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, -90);
        }
        else if (_isHangedRight && !_isGrounded)
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 90);
        }
        else
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0);
        }
    }

    private void CheckFalling()
    {
        _isFalling = _rigidBody2D.velocity.y < 0 && !_isGrounded;
    }

    public void Update()
    {
        CheckGround();
        CheckDoubleJump();
        CheckFalling();
        CheckHanged();
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
            if (_velocity_X > 0)
            {
                GetComponent<SpriteRenderer>().flipX = false;
            }
            else if (_velocity_X < 0)
            {
                GetComponent<SpriteRenderer>().flipX = true;
            }
        }
    }

    void FixedUpdate()
    {
        _rigidBody2D.velocity = new Vector2(_velocity_X * Time.fixedDeltaTime, _rigidBody2D.velocity.y);
    }
}

using System;
using System.Collections;
using UnityEngine;
using static PlayerController;

public class PlayerActions : MonoBehaviour, IGameplayActions
{
    #region Properties
    [Range(10, 1000f)] [SerializeField] private float playerMovementSpeed = 100f;
    [Range(10, 1000f)] [SerializeField] private float playerJumpSpeed = 300f;
    [SerializeField] private bool _canMoveinAir = true;
    [SerializeField] private State state;

    public bool isGrounded, isMoving, isJumping, isLanding;
    private bool canMove = true;
    public int _groundMask;
    private PlayerController _controller;
    private Rigidbody2D _rigidBody;
    private Animator _animator;
    private float _vectorSpeed_X;
    #endregion
    public void OnEnable()
    {
        _controller?.Enable();
    }
    public void OnDisable()
    {
        _controller?.Disable();
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
        if (context.performed)
        {
            _animator.SetBool("isJumping", true);
            _animator.SetBool("isGrounded", false);
            Vector2 forceJump = Vector2.up * playerJumpSpeed;
            _rigidBody.AddForce(forceJump);
        }
    }
    public void OnMove(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        _vectorSpeed_X = context.ReadValue<float>() * playerMovementSpeed;

        if (_vectorSpeed_X == 0)
        {
            isMoving = false;
            _animator.SetBool("isMoving", false);
        }
        else
        {
            isMoving = true;
            _animator.SetBool("isMoving", true);
        }

    }

    public void FixedUpdate()
    {
        if (canMove)
        {
            _rigidBody.velocity = new Vector2(_vectorSpeed_X * Time.fixedDeltaTime, _rigidBody.velocity.y);
        }
        else
        {
            _rigidBody.velocity = new Vector2(0 * Time.fixedDeltaTime, _rigidBody.velocity.y);
        }
    }

    public void Start()
    {
        _groundMask = LayerMask.GetMask("Ground");
    }

    public void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _controller = new PlayerController();
        _controller.Gameplay.SetCallbacks(this);
    }

    public void Update()
    {
        if (_vectorSpeed_X > 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (_vectorSpeed_X < 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
        if (GetComponent<Rigidbody2D>().velocity.y < 0)
        {
            isGrounded = Physics2D.Raycast(GetComponent<CapsuleCollider2D>().bounds.center, Vector2.down, GetComponent<CapsuleCollider2D>().size.y, _groundMask);
            _animator.SetBool("isGrounded", isGrounded);
        }
        else
        {
            isGrounded = false;
        }
    }
    public void Land()
    {
        isJumping = false;

        _animator.SetBool("isJumping", false);
    }

    public void ManageMovement(int enable)
    {
        canMove = (enable == 1) ? true : false;
        Debug.Log(String.Format("Can Move? {0}", canMove));
    }

    public float GetMovementSpeed()
    {
        return _vectorSpeed_X;
    }

    public float GetJumpSpeed()
    {
        return playerJumpSpeed;
    }

    public bool CanMoveInAir()
    {
        return _canMoveinAir;
    }



}

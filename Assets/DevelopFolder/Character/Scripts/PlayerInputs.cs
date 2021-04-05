
using UnityEngine;
using static InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class PlayerInputs : MonoBehaviour, IGameplayActions
{
    [SerializeField] public float speed, jumpForce, maxJumpTime;
    private InputSystem _inputs;
    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private CircleCollider2D _collider;
    private float jumpTime;
    [SerializeField] private float moveDirection;
    [SerializeField] private bool isGrounded, isJumping, checkForLanding, jumpActionExecute, isLanded, canMove;
    private int groundMask;
    private float groundCheckError = 0.01f;

    #region InputEvents
    public void OnAttack(CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnJump(CallbackContext context)
    {
        isJumping = context.performed;
        jumpActionExecute = isGrounded && isJumping;
        if (jumpActionExecute)
        {
            Debug.Log("JumpCommand!");
            jumpTime = maxJumpTime;
            checkForLanding = true;
        }
    }

    public void OnMove(CallbackContext context)
    {
        moveDirection = context.ReadValue<float>();
    }

    public void OnHide(CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    private void OnEnable()
    {
        if (_inputs == null)
        {
            _inputs = new InputSystem();
            _inputs.gameplay.SetCallbacks(this);
        }
        _inputs.gameplay.Enable();
    }

    private void OnDisable()
    {
        _inputs.gameplay.Disable();
    }
    #endregion
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<CircleCollider2D>();
        _animator = GetComponent<Animator>();
        groundMask = LayerMask.GetMask("Ground");
        canMove = true;
    }

    private void Update()
    {
        ManageAnimations();

        isGrounded = Physics2D.Raycast(_collider.bounds.center, Vector2.down, _collider.radius + groundCheckError, groundMask);

        if (moveDirection > 0)
            GetComponent<SpriteRenderer>().flipX = true;
        else if (moveDirection < 0)
            GetComponent<SpriteRenderer>().flipX = false;
    }
    private void FixedUpdate()
    {
        if (canMove)
            _rigidbody.velocity = new Vector2(moveDirection * speed * Time.deltaTime, _rigidbody.velocity.y);

        if (jumpActionExecute)
        {
            jumpActionExecute = false;
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, jumpForce * Time.deltaTime);
        }

        if (jumpTime > 0 && isJumping)
        {
            jumpTime -= Time.deltaTime;
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, jumpForce * Time.deltaTime);
        }
    }

    public void CanMove(int canMove)
    {
        this.canMove = canMove == 1;
        if (!this.canMove)
        {
            _rigidbody.velocity = Vector2.zero;
        }
    }


    private void ManageAnimations()
    {
        _animator.SetBool("isJumping", isJumping);
        _animator.SetBool("isGrounded", isGrounded);
        _animator.SetInteger("moveDirection", (int)moveDirection);
    }
}

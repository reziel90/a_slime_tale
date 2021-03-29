using System;
using UnityEngine;
using static PlayerController;

public class PlayerActions : MonoBehaviour, IGameplayActions
{
    #region Properties
    [Range(10, 1000f)] [SerializeField] private float playerMovementSpeed = 100f;
    [Range(10, 1000f)] [SerializeField] private float playerJumpSpeed = 300f;
    [SerializeField] private bool _canMoveinAir = true;

    public bool isGrounded, isDoubleJump, isMoving, isJumping;
    private int _groundMask;
    private PlayerController _controller;
    private float _vectorSpeed_X;
    [Range(0, 5)] [SerializeField] private float _groundDistance = 0.4f;
    private StatesManager statesManager;
    #endregion

    #region ControllerEvents
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
            isJumping = true;
            Debug.Log("IsJumping = True;");
        }
    }
    public void OnMove(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        float amount = context.ReadValue<float>();
        if (amount == 0)
        {
            isMoving = false;
        }
        else
        {
            isMoving = true; //quando metterlo false?
            _vectorSpeed_X = amount * playerMovementSpeed;
        }
        Debug.Log(context);
    }

    #endregion

    public void Start()
    {
        statesManager = new StatesManager();
        statesManager.SetNextState(new Idle(this.gameObject, statesManager));
        _groundMask = LayerMask.GetMask("Ground");
    }
    public void Awake()
    {

        _controller = new PlayerController();
        _controller.Gameplay.SetCallbacks(this);
    }
    public void Update()
    {
        CheckGround();
        statesManager.Update();
    }
    public void FixedUpdate()
    {
        statesManager.FixedUpdate();
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
    private void CheckGround()
    {
        bool prevState = isGrounded;
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, _groundDistance, _groundMask).collider != null ? true : false;
        //isJumping = prevState && isGrounded ? false : isJumping;
        Debug.DrawRay(transform.position, Vector2.down, Color.red, _groundDistance);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : State
{
    private float _jumpSpeed;
    private float _speed;
    public Jump(GameObject character, StatesManager manager) : base(character, manager)
    {
        _jumpSpeed = _player.GetJumpSpeed();
        _speed = _player.GetMovementSpeed();
    }

    public override void OnEnterState()
    {
        _animator.Play("Jumping_rise");
        Vector2 forceJump = new Vector2(_rb.velocity.x, _jumpSpeed);
        _rb.AddForce(forceJump);
    }

    public override void StateUpdate()
    {
        if (_player.isGrounded)
        {
            _manager.SetNextState(new Landing(_character, _manager));
        }
    }
    public override void StateFixedUpdate()
    {
        if (_player.CanMoveInAir()) _rb.velocity = new Vector2(_speed * Time.fixedDeltaTime, _rb.velocity.y);
    }
}

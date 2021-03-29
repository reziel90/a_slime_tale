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
        Vector2 forceJump = Vector2.up * _jumpSpeed;
        _rb.AddForce(forceJump);
    }

    public override void StateUpdate()
    {
        CheckDirectionAndFlip();
        if (_player.isGrounded)
        {
            _manager.SetNextState(new Landing(_character, _manager));
        }
    }
    public override void StateFixedUpdate()
    {
        if (_player.CanMoveInAir())
        {
            _rb.velocity = new Vector2(_speed * Time.fixedDeltaTime, _rb.velocity.y);
        }
    }

    private void CheckDirectionAndFlip()
    {
        if (_speed > 0)
        {
            _spriteRenderer.flipX = true;
        }
        else if (_speed < 0)
        {
            _spriteRenderer.flipX = false;
        }
    }
}

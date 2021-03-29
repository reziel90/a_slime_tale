using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Landing : State
{
    public Landing(GameObject character, StatesManager manager) : base(character, manager)
    {
    }

    public override void OnEnterState()
    {
        _rb.velocity = new Vector2(0, _rb.velocity.y);
        _animator.Play("Landing");
    }

    public override void StateUpdate()
    {
        if (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {
            _player.isJumping = false;
            _manager.SetNextState(new Idle(_character, _manager));
        }
    }
}
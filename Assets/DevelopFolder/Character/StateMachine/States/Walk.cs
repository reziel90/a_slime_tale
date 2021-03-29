using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walk : State
{

    private float _speed;
    public Walk(GameObject character, StatesManager manager) : base(character, manager)
    {
        _speed = _player.GetMovementSpeed();
    }

    public override void OnEnterState()
    {
        _animator.Play("Walk"); //Non voglio usare le stringhe per le animazioni!
    }

    public override void OnExitState()
    {
        base.OnExitState();
    }

    public override void StateUpdate()
    {
        CheckDirectionAndFlip();
        if (!_player.isMoving)
        {
            _manager.SetNextState(new Idle(_character, _manager));
        }
        if (_player.isJumping)
        {
            _manager.SetNextState(new Jump(_character, _manager));
        }
        //se sono fermo torno in Idle
        //se salto vado in Jump
        //...
    }

    public override void StateFixedUpdate()
    {
        _rb.velocity = new Vector2(_speed * Time.fixedDeltaTime, _rb.velocity.y);
    }

    private void CheckDirectionAndFlip()
    {
        if (_speed > 0)
        {
            _character.GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (_speed < 0)
        {
            _character.GetComponent<SpriteRenderer>().flipX = false;
        }
    }

}

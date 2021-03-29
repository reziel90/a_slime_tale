using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : State
{

    public Idle(GameObject character, StatesManager manager) : base(character, manager)
    {
    }

    public override void OnEnterState()
    {
        _rb.velocity = new Vector2(0, _rb.velocity.y);
        _animator.Play("Idle"); //Non voglio usare le stringhe per le animazioni!
    }

    public override void OnExitState()
    {
        base.OnExitState();
    }

    public override void StateUpdate()
    {
        if (_player.isMoving)
        {
            _manager.SetNextState(new Walk(_character, _manager));
        }
        if (_player.isJumping)
        {
            _manager.SetNextState(new Jump(_character, _manager));
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    protected GameObject _character;
    protected Rigidbody2D _rb;
    protected PlayerActions _player;
    protected Animator _animator;
    protected StatesManager _manager;
    protected SpriteRenderer _spriteRenderer;

    public State(GameObject character, StatesManager manager)
    {
        //brutti i nomi, da migliorare
        _character = character;
        _rb = character.GetComponent<Rigidbody2D>();
        _player = character.GetComponent<PlayerActions>();
        _animator = character.GetComponent<Animator>();
        _spriteRenderer = _player.GetComponent<SpriteRenderer>();
        _manager = manager;
    }

    public virtual void StateUpdate()
    {
    }

    public virtual void StateFixedUpdate()
    {
    }

    public virtual void OnEnterState()
    {
    }
    public virtual void OnExitState()
    {
    }

}

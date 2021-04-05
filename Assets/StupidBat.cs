using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
public class StupidBat : MonoBehaviour
{
    [SerializeField] public float aggroDistance, huntingTime, speedCorrection, roofCheckDistance, risingSpeed;

    private int groundMask, playerMask;
    private float _huntingTime;
    private bool isHunting = false;
    private bool isGoingBack = false;
    private bool isHanged = true;
    private Animator _animator;
    private Rigidbody2D _rigidbody;
    private Transform target;

    private float startingGravity;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        groundMask = LayerMask.GetMask("Ground");
        startingGravity = _rigidbody.gravityScale;
        InvokeRepeating("AI", 0f, huntingTime);
    }
    void Update()
    {
        Debug.DrawRay(_rigidbody.position, Vector2.up, Color.green, roofCheckDistance);
        isHanged = Physics2D.Raycast(_rigidbody.position, Vector2.up, roofCheckDistance, groundMask);
        _animator.SetBool("isHanged", isHanged);
        if (_rigidbody.velocity.x > 0)
        {
            GetComponent<SpriteRenderer>().flipX = true; ;
        }
        else if (_rigidbody.velocity.x < 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
        if (_rigidbody.velocity.y > 0)
        {
            _rigidbody.gravityScale = risingSpeed;
        }
        else
        {
            _rigidbody.gravityScale = startingGravity;
        }
    }

    private void AI()
    {
        if (isHanged)
        {
            float targetDistance = Vector2.Distance(target.position, _rigidbody.position);
            if (targetDistance < aggroDistance)
            {
                Vector2 targetDirection = ((Vector2)target.position - _rigidbody.position).normalized;
                _rigidbody.AddForce(targetDirection * targetDistance * speedCorrection);
            }
        }

    }
}

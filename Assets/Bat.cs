using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
public class Bat : MonoBehaviour
{
    private Seeker _pathSeeker;
    [SerializeField] public float aggroDistance;
    [SerializeField] public float huntingTime;
    [SerializeField] public float speed;
    private float nextWaypointDistance = 3f;

    private Path path;
    private int currentWaypoint;
    private bool reachedEndWaypoint;
    private int groundMask, playerMask;
    private float _huntingTime;
    private bool isHunting = false;
    private bool isGoingBack = false;
    private bool isHanged = true;

    private Animator _animator;
    private Rigidbody2D _rigidbody;

    void Start()
    {
        _pathSeeker = GetComponent<Seeker>();
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        _pathSeeker.StartPath(_rigidbody.position, player.transform.position, OnPathComplete);
    }

    void OnPathComplete(Path p)
    {
        if (p.error)
            return;
        path = p;
        currentWaypoint = 0;
    }
    void Update()
    {
        AI();
        ManageAnimations();
    }
    private void ManageAnimations()
    {
        _animator.SetBool("isHanged", isHanged);

    }
    private void AI()
    {
        if (path == null) return;

        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndWaypoint = true;
            return;
        }
        else
        {
            reachedEndWaypoint = false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - _rigidbody.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        _rigidbody.AddForce(force);

        float distance = Vector2.Distance(_rigidbody.position, path.vectorPath[currentWaypoint]);
        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }
    }

    private void StateManagement()
    {
        if (isHanged)
        {
            isGoingBack = false;
            isHunting = true;
            isHanged = false;
            _huntingTime = huntingTime;
        }

        if (isHunting)
        {
            if (_huntingTime > 0)
            {
                _huntingTime -= Time.deltaTime;
            }
            else
            {
                isHunting = false;
                isGoingBack = true;
            }
        }

        if (isGoingBack)
        {
            isHanged = Physics2D.Raycast(transform.position, Vector2.up, GetComponent<CircleCollider2D>().radius, groundMask);
        }
    }
}

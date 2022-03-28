using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public int health;
    public int speed;

    [SerializeField] private Transform target;
    private NavMeshAgent agent;
    private Animator _animator;

    public bool isMoving = true;

    private bool isCanHit = true;
    
    void Start()
    {
        _animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        target = GameObject.Find("Player").transform;
    }

    private void Update()
    {
        if (target == null)
            return;
        if (!isMoving)
        {
            return;
        }
        if (Vector3.Distance(target.position, transform.position) < 0.7f)
        {
            agent.SetDestination(transform.position);
            _animator.SetBool("isRunning", false);
            if (isCanHit)
            {
                target.GetComponent<PlayerController>().TakeDamage(3);
                isCanHit = false;
                Invoke("SetIsCanHitTrue", 1f);
            }
        }
        else
        {
            agent.SetDestination(target.position);
            _animator.SetBool("isRunning", true);
        }
        
        if (health <= 0)
        {
            agent.SetDestination(transform.position);
            _animator.SetBool("isDying", true);
            Invoke("Die", 0.4f);
        }
    }

    private void SetIsCanHitTrue()
    {
        isCanHit = true;
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
    }
}

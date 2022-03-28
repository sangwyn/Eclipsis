using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyDistanceAttack : MonoBehaviour
{
    public int health;
    public int speed;

    public float attackDistance;
    [SerializeField] private Transform target;
    [SerializeField] private LayerMask whatIsSolid;
    private NavMeshAgent agent;

    private bool isInAttackDistance = false;
    public bool isShooting = false;
    private float shootingPeriod;
    private float shootingStartTime;
    private bool isGoingBack = false;
    private bool isChangingDestination = false;

    private bool isStart = true;
    private Vector3 prevPosition;

    private Vector3 roamingPosition;
    
    private Animator _animator;

    public bool isMoving = true;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        target = GameObject.Find("Player").transform;
        shootingPeriod = 1;
    }

    private void Update()
    {
        if (target == null)
            return;
        if (!isMoving)
        {
            return;
        }

        if (!isStart)
        {
            if (transform.position == prevPosition)
            {
                roamingPosition = transform.position;
            }
        }
        else
        {
            isStart = false;
        }
        
        

        if (isChangingDestination)
        {
            if (Vector3.Distance(transform.position, roamingPosition) < 1f)
            {
                isChangingDestination = false;
                agent.SetDestination(target.position);
                _animator.SetBool("isRunning", true);
            }
        }

        if (isGoingBack)
        {
            _animator.SetBool("isRunning", true);
            if (Vector3.Distance(transform.position, roamingPosition) < 1f)
            {
                isGoingBack = false;
                ChangeDestination();
            }
        }

        if (isShooting)
        {
            
            _animator.SetBool("isRunning", false);
            if (Vector3.Distance(transform.position, roamingPosition) < 0.1f)
            {
                roamingPosition = Random.insideUnitCircle;
                agent.SetDestination(roamingPosition);
            }
            
            //shhot
            //......
            if (Time.time - shootingStartTime >= shootingPeriod)
            {
                isShooting = false;
                GoBack();
            }
        }

        if ((transform.position - target.position).magnitude > attackDistance && !isGoingBack && !isShooting && !isChangingDestination)
        {
            _animator.SetBool("isRunning", true);
            agent.SetDestination(target.position);
            isInAttackDistance = false;
        } else
        {
            isInAttackDistance = true;
            if (!isShooting && !isGoingBack && !isChangingDestination)
            {
                agent.SetDestination(transform.position);
                _animator.SetBool("isRunning", false);
                Shoot();
            }
        }

        prevPosition = transform.position;
        
        if (health <= 0)
        {
            agent.SetDestination(transform.position);
            _animator.SetBool("isDying", true);
            Invoke("Destroy_enemy", 0.4f);
        }
    }

    private void Destroy_enemy()
    {
        Destroy(gameObject);
    }
    
    private void GoBack()
    {
        isGoingBack = true;
        roamingPosition = transform.position + (transform.position - target.position) * Random.Range(0.5f, 2);
        agent.SetDestination(roamingPosition);
    }

    private void Shoot()
    {
        isShooting = true;
        shootingStartTime = Time.time;
        roamingPosition = transform.position;
        shootingPeriod = Random.Range(1, 2);
    }

    private void ChangeDestination()
    {
        isChangingDestination = true;
        roamingPosition = Random.insideUnitCircle * 10;
        agent.SetDestination(roamingPosition);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
    }
}

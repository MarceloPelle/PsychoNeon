using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyAI : MonoBehaviour
{
    public GameObject weaponPosition;

    TimeBody timeBody;

    public NavMeshAgent agent;

    public Transform player;

    PlayerStats playerstats;

    public LayerMask whatIsGround, whatIsPlayer;

    public float enemySpeed;

    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;
    private float notWalk;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject projectile;
    public float damage = 20f;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;
    public bool IsStopped;

    private void Awake()
    {
        
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        timeBody = GetComponent<TimeBody>();
    }

    private void Update()
    {
        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange && timeBody.IsStopped == false)
        {
            Patroling();
        }
        if(playerInSightRange && !playerInAttackRange && timeBody.IsStopped == false)
        {
            agent.speed = enemySpeed;
            ChasePlayer();
        }
        else if(timeBody.IsStopped)
        {
            agent.speed = 0;
        }
        if (playerInAttackRange && playerInAttackRange && timeBody.IsStopped == false)
        {
            AttackPlayer();
        }
    }

    private void Patroling()
    {
        if(!walkPointSet)
        {
            SearchWalkPoint();
        }

        if(walkPointSet)
        {
            agent.SetDestination(walkPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if(distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
        }
    }
    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if(Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        {
            walkPointSet = true;
        }
    }
    private void ChasePlayer()
    {
        if(timeBody.IsStopped == false)
        {
            agent.isStopped = false;
            agent.SetDestination(player.position);
        }
        else
        {
            agent.isStopped = true;
        }
            
    }
    private void AttackPlayer()
    {
        //Make sure enemy not move
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if(!alreadyAttacked)
        {
            Instantiate(projectile, weaponPosition.transform.position, Quaternion.identity);

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

}

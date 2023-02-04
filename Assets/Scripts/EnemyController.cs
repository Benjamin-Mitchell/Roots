using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EnemyController : Character
{
    private NavMeshAgent agent;
    private Transform player;
    public LayerMask whatIsGround, whatIsPlayer;
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public float sightRange, attackRange;
    private bool playerInSightRange, playerInAttackRange; 

    private int currentHealth;
    private Rigidbody rb;
    public Weapon weapon;
    public float minDistance =2f;

    private bool canAttack;

    private RoomGenerator roomGenerator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        currentHealth = maxHealth;
    }

    private void Start()
    {
        StartCoroutine(nameof(AttackStartup));
    }

    private IEnumerator AttackStartup()
    {
        yield return new WaitForSeconds(2);
        canAttack = true;
    }

    private void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange) Patrolling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInAttackRange && playerInSightRange) AttackPlayer();
    }


    public override void PerformAttack()
    {
        weapon.PerformAttack(player.position);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, sightRange);
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    private void Patrolling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if(walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if(distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
        }
    }

    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z);

        if(Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        {
            walkPointSet = true;
        }
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
        transform.LookAt(player);
    }
    private void AttackPlayer()
    {
        if (!alreadyAttacked && canAttack)
        {
            PerformAttack();

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }

        if (Vector3.Distance(transform.position, player.position) > minDistance)
        {
            ChasePlayer();
        }
        else
        {
            agent.velocity = Vector3.zero;
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public override void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if(currentHealth <= 0)
        {
            Die();
        }
    }

    public void SetRoomGeneration(RoomGenerator generator)
	{
        roomGenerator = generator;
    }

    public override void Die()
    {
        roomGenerator.aliveEnemies.Remove(this);
        Destroy(gameObject);
    }

    public override void Knockback(Vector3 direction)
    {
        //var force = (direction.normalized * 10) + Vector3.up * 120;
        ////var force = new Vector3(0, 10, 10);
        //rb.AddForce(force, ForceMode.Impulse);
    }

}

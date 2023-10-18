using RayFire;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnicornAI : MonoBehaviour
{
    public NavMeshAgent agent;

    private Transform player;

    [SerializeField] private Rigidbody rb;

    public LayerMask whatIsGround, whatIsPlayer;

    public float health;

    Vector3 playerPosition;

    public int dashSpeed;

    private bool chasePlayer = false;

    // Attacking
    public float timeBetweenAttacks;
    private bool alreadyAttacked;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void LookUpdate()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, 1000))
        {
            Collider[] overlap = Physics.OverlapSphere(hit.point, 0.1f);

            for (int i = 0; i < overlap.Length; i++)
            {
                if (overlap[i].CompareTag("Player"))
                {
                    if (!chasePlayer)
                    {
                        playerPosition = overlap[i].transform.position;
                        transform.LookAt(playerPosition);
                        chasePlayer = true;
                    }
                }
            }
    }
}

    private void Update()
    {
        LookUpdate();

        if (!chasePlayer)
            ChasePlayer();
        else
            GoToPlayer();
    }

    private void ChasePlayer()
    {
        rb.AddForce(transform.forward * dashSpeed, ForceMode.Impulse);
        chasePlayer = false;
    }

    private void GoToPlayer()
    {
        agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        // Stop moving enemies
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        if (health < 0f) Invoke(nameof(DestroyEnemy), .5f);
    }

    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }

}

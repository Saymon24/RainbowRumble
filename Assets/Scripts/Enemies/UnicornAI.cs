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

    public int dashSpeed = 2;
    public float timeBetweenDash = 3f;
    public float dashDuration = 5f;

    private bool canDash = true;
    private bool isDashing = false;
    private float dashClockStart = 0f;

    // Attacking
    public float timeBetweenAttacks;
    private bool alreadyAttacked;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        dashClockStart = Time.time;
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
                        if (canDash && !isDashing)
                        {
                            agent.enabled = false;
                            playerPosition = overlap[i].transform.position;
                            canDash = false;
                            isDashing = true;
                            rb.AddForce(transform.forward * dashSpeed, ForceMode.Impulse);
                            dashClockStart = Time.time;
                        }
                }
            }
        }
    }

    private void updateDash()
    {
        float elapsedTime = dashClockStart - Time.time;

        if (isDashing)
        {
            if (elapsedTime > dashDuration)
            {
                isDashing = false;
                rb.velocity = new Vector3(0,0,0);
                agent.enabled = true;
                dashDuration = Time.time;
            }
        } else
        {
            if (elapsedTime > timeBetweenDash)
            {
                canDash = true;
                dashClockStart = Time.time;
            }
        }
    }

    private void Update()
    {
        LookUpdate();
        updateDash();

        if (!isDashing)
            GoToPlayer();
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

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

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.collider.CompareTag("Enemy"))
        {
            StopDashing();
        }
    }

    private void LookUpdate()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, 50))
        {
            Collider[] overlap = Physics.OverlapSphere(hit.point, 0.1f);

            for (int i = 0; i < overlap.Length; i++)
            {
                if (overlap[i].CompareTag("Player"))
                {
                        if (canDash && !isDashing)
                        {
                            agent.enabled = false;
                            rb.constraints = RigidbodyConstraints.FreezeRotation;
                            playerPosition = overlap[i].transform.position;
                            canDash = false;
                            isDashing = true;
                            dashClockStart = Time.time;
                        }
                }
            }
        }
    }

    private void StopDashing()
    {
        isDashing = false;
        rb.constraints = RigidbodyConstraints.None;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.Sleep();
        dashClockStart = Time.time;
        agent.enabled = true;
    }

    private void updateDash()
    {
        float elapsedTime = Time.time - dashClockStart;

        if (isDashing)
        {
            if (elapsedTime > dashDuration)
                StopDashing();
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

    private void FixedUpdate()
    {
        if (isDashing)
            rb.MovePosition(transform.position + (transform.forward * Time.deltaTime * dashSpeed));
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
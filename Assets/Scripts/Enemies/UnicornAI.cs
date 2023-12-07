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

    Vector3 playerPosition;

    private Animator animator;

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
        animator = GetComponentInChildren<Animator>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.collider.CompareTag("Enemy") && !collision.collider.CompareTag("Untagged"))
        {
            if (collision.collider.CompareTag("Player"))
            {
                if (!alreadyAttacked)
                {
                    alreadyAttacked = true;
                    GetComponent<Enemy>().AttackPlayer();
                    Invoke(nameof(ResetAttack), timeBetweenAttacks);
                }
            }
            HandleAttack();
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
                            animator.SetBool("IsDashing", true);
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
        animator.SetBool("IsDashing", false);
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
        if (GetComponent<Enemy>().isDead)
            return;

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
        if (!agent.SetDestination(player.position))
            Destroy(this.gameObject);
    }

    private void HandleAttack()
    {
        agent.enabled = true;
        if (!agent.SetDestination(player.position))
            Destroy(this.gameObject);
        agent.enabled = false;
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

}

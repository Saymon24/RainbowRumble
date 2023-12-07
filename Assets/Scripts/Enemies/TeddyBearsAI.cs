using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class TeddyBearsAI : MonoBehaviour
{
    public NavMeshAgent agent;
    [SerializeField] private Rigidbody rb;
    private Animator animator;

    private Transform player;

    public LayerMask whatIsGround, whatIsPlayer;

    // Attacking
    public float timeBetweenAttacks;
    private bool alreadyAttacked;

    // States
    public float attackRange;
    public bool playerInAttackRange;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (GetComponent<Enemy>().isDead)
            return;

        // Check for sight and attack range
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInAttackRange) ChasePlayer();
        else HandleAttack();

}

    private void ChasePlayer()
    {
        if (!agent.SetDestination(player.position))
            Destroy(this.gameObject);
    }

    private void HandleAttack()
    {
        // Stop moving enemies
        agent.SetDestination(transform.position);
        animator.SetBool("IsAttack", true);

        //transform.LookAt(player);

        if (!alreadyAttacked)
        {
            alreadyAttacked = true;
            GetComponent<Enemy>().AttackPlayer();
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
        animator.SetBool("IsAttack", false);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}

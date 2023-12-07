using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class BossTeddyBearsAI : MonoBehaviour
{
    public NavMeshAgent agent;
    //private NavMeshPath path;
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

    // Test

    public float spreadAmount = 20.0f; // Ajustez cette valeur pour contrôler l'écartement.

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

        if (agent.hasPath)
        {
            Vector3[] waypoints = agent.path.corners;
            for (int i = 0; i < waypoints.Length - 1; i++)
            {
                Debug.DrawLine(waypoints[i], waypoints[i + 1], Color.red);
            }
        }

        // Check for sight and attack range
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (GetComponent<BreakWalls>().BehindWall()) StopDestination();
        else if (!playerInAttackRange) ChasePlayer();
        else HandleAttack();

    }

    private void StopDestination()
    {
        agent.SetDestination(transform.position);
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

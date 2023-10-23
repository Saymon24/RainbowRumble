using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class BossTeddyBearsAI : MonoBehaviour
{
    public NavMeshAgent agent;
    [SerializeField] private Rigidbody rb;

    private Transform player;

    public LayerMask whatIsGround, whatIsPlayer;

    // Attacking
    public float timeBetweenAttacks;
    private bool alreadyAttacked;

    // States
    public float attackRange;
    public bool playerInAttackRange;

    // Test

    public float spreadAmount = 1.0f; // Ajustez cette valeur pour contrôler l'écartement.

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void FixedUpdate()
    {
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
        else AttackPlayer();

    }

    private void StopDestination()
    {
        agent.SetDestination(transform.position);
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);

        Vector3 destination = player.position;

        // Générez un chemin avec CalculatePath.
/*        NavMeshPath path = new NavMeshPath();
        if (NavMesh.CalculatePath(transform.position, destination, NavMesh.AllAreas, path))
        {
            // Ajustez les positions des waypoints du chemin.
            for (int i = 0; i < path.corners.Length; i++)
            {
                Vector3 originalPosition = path.corners[i];
                Vector3 offset = (Random.insideUnitSphere * spreadAmount);
                path.corners[i] = originalPosition + offset;
            }

            // Appliquez le chemin ajusté à l'agent.
            Debug.Log(agent.SetPath(path));
        }*/
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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}

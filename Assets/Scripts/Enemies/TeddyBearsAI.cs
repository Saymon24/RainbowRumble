using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class TeddyBearsAI : MonoBehaviour
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

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        // Check for sight and attack range
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInAttackRange) ChasePlayer();
        else HandleAttack();

}

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    private void HandleAttack()
    {
        // Stop moving enemies
        agent.SetDestination(transform.position);

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
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}

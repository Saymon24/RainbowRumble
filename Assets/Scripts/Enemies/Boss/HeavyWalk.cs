using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HeavyWalk : MonoBehaviour
{

    private Transform playerTransform;

    [SerializeField] private LayerMask whatIsPlayer;

    [Header("Walk Settings")]
    [SerializeField] private float walkRange = 5f;

    void Start()
    {
        playerTransform = GameObject.Find("Player").transform;
    }

    private bool isPlayerInRange()
    {
        return Physics.CheckSphere(transform.position, walkRange, whatIsPlayer);
    }

    private void applyHeavyWalkEffect()
    {
        float dist = Vector3.Distance(playerTransform.position, transform.position);

        if (isPlayerInRange())
        {

        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, walkRange);
    }
}

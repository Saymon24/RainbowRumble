using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using EZCameraShake;

public class HeavyWalk : MonoBehaviour
{

    private Transform playerTransform;

    //private CameraShake cameraShake;
    [SerializeField] private LayerMask whatIsPlayer;

    [Header("Walk Settings")]
    [SerializeField] private float walkRange = 5f;

    void Start()
    {
        playerTransform = GameObject.Find("Player").transform;

        // For Testing
        //startTime = Time.time;
    }

    private bool isPlayerInRange()
    {
        return Physics.CheckSphere(transform.position, walkRange, whatIsPlayer);
    }

    public void applyHeavyWalkEffect()
    {
        float dist = Vector3.Distance(playerTransform.position, transform.position);

        if (isPlayerInRange())
        {
            CameraShaker.Instance.ShakeOnce(10 / dist, 4f, 0.5f, 0.5f);
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, walkRange);
    }
}

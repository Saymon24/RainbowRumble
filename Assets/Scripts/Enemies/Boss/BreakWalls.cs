using EZCameraShake;
using RayFire;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BreakWalls : MonoBehaviour
{
    private Transform playerTransform;

    [Header("Settings")]
    [SerializeField] private LayerMask breakableWallsLayer;
    [SerializeField] private RayfireGun handsGun;
    [SerializeField] private RayfireGun legsGun;
    [SerializeField] private float range;

    [SerializeField] private Transform handsPosition;
    [SerializeField] private Transform legsPosition;

    //private CameraShake cameraShake;
    [SerializeField] private LayerMask whatIsPlayer;

    [Header("Range Settings")]
    [SerializeField] private float shakeRange = 5f;

    private Ray handRay;
    private Ray legRay;

    private bool handsHasWall = false;
    private bool legsHasWall = false;

    // For Testing
    private float startCounter = 0.0f;

    private void Start()
    {
        playerTransform = GameObject.Find("Player").transform;
    }

    public bool BehindWall()
    {
        return handsHasWall || legsHasWall;
    }

    public void breakWallsWithArms()
    {
        handsGun.Shoot();
        applyShakeEffect();
    }

    public void breakWallsWithLegs()
    {
        legsGun.Shoot();
        applyShakeEffect();
    }

    private bool isPlayerInRange()
    {
        return Physics.CheckSphere(transform.position, shakeRange, whatIsPlayer);
    }

    public void applyShakeEffect()
    {
        float dist = Vector3.Distance(playerTransform.position, transform.position);

        if (isPlayerInRange())
        {
            CameraShaker.Instance.ShakeOnce(30 / dist, 4f, 0.5f, 0.5f);
        }
    }

    private void checkHands()
    {
        float t = Time.time - startCounter;
        RaycastHit hit;
        handRay = new Ray(handsPosition.position, transform.TransformDirection(Vector3.forward) * range);

        Debug.DrawRay(handRay.origin, handRay.direction * range, Color.magenta);

        if (Physics.Raycast(handRay, out hit, range, breakableWallsLayer))
        {
            if (!handsHasWall)
                startCounter = Time.time;
            handsHasWall = true;
        }
        else
            handsHasWall = false;

        if (handsHasWall && t > 0.01f)
        {
            breakWallsWithArms();
            startCounter = Time.time;
        }
    }

    private void checkLegs()
    {
        float t = Time.time - startCounter;
        RaycastHit hit;
        legRay = new Ray(legsPosition.position, transform.TransformDirection(Vector3.forward) * range);

        Debug.DrawRay(legRay.origin, legRay.direction * range, Color.red);

        if (Physics.Raycast(legRay, out hit, range, breakableWallsLayer))
        {
            if (!legsHasWall)
                startCounter = Time.time;
            legsHasWall = true;
        }
        else
            legsHasWall = false;

        if (legsHasWall && t > 0.01f)
        {
            breakWallsWithLegs();
            startCounter = Time.time;
        }
    }

    void Update()
    {
        checkHands();
        checkLegs();
    }
}

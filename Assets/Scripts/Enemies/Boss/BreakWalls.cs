using RayFire;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BreakWalls : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private LayerMask breakableWallsLayer;
    [SerializeField] private RayfireGun handsGun;
    [SerializeField] private RayfireGun legsGun;
    [SerializeField] private float range;

    [SerializeField] private Transform handsPosition;
    [SerializeField] private Transform legsPosition;

    private Ray handRay;
    private Ray legRay;

    private bool handsHasWall = false;
    private bool legsHasWall = false;

    // For Testing
    private float startCounter = 0.0f;

    public bool BehindWall()
    {
        return handsHasWall || legsHasWall;
    }

    public void breakWallsWithArms()
    {
        handsGun.Shoot();   
    }

    public void breakWallsWithLegs()
    {
        legsGun.Shoot();
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

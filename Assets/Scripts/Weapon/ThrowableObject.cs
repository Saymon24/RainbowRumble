using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableObject : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform Fpscam;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private GameObject objectToThrow;

    [SerializeField] private GunData gunData;

    private bool readyToThrow;

    private float timerToRefill = 0f;
    private bool refilling = false;

    private void Start()
    {
        readyToThrow = true;
    }

    private void Update()
    {
        if (Input.GetButton("Throw") && readyToThrow && gunData.currentAmmo > 0)
        {
            Throw();
        }

        Refill();
    }

    private void Throw()
    {
        readyToThrow = false;

        GameObject projectile = Instantiate(objectToThrow, attackPoint.position, Fpscam.rotation);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();

        Vector3 forceToApply = Fpscam.transform.forward * gunData.throwForce + transform.up * gunData.throwUpwardForce;

        rb.AddForce(forceToApply, ForceMode.Impulse);

        gunData.currentAmmo--;

        Invoke(nameof(ResetThrowCooldown), gunData.throwCooldown);
    }

    private void ResetThrowCooldown()
    {
        readyToThrow = true;
    }

    private void Refill()
    {

        if (gunData.currentAmmo >= gunData.magSize)
            return;

        if (!refilling)
        {
            timerToRefill = Time.time;
            refilling = true;
        }

        if (Time.time - timerToRefill >= gunData.reloadTime)
        {
            gunData.currentAmmo++;
            refilling = false;
        }
    }

    public float GetRefillTimer()
    {
        return gunData.reloadTime - (Time.time - timerToRefill);
    }

    public int GetCurrentNumberofThrowableAvailaible()
    {
        return (int)gunData.currentAmmo;
    }

}

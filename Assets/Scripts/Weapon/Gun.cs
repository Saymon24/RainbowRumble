using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{

    [Header("Gun Settings")]
    [SerializeField] float damage = 10f;
    [SerializeField] float range = 100f;
    [SerializeField] float firerate = 200f;
    [SerializeField] float impactForce = 10f;
    [SerializeField] Transform gunEnd;
    [SerializeField] GameObject muzzleFlash;
    [SerializeField] GameObject impactEffect;

    [SerializeField] Camera fpsCam;

    private float nextTimeToFire = 0f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / firerate;
            Shoot();
        }
    }

    private void Shoot()
    {

        muzzleFlash.GetComponent<ParticleSystem>().Play();

        RaycastHit hit;

        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {


            if (hit.transform.gameObject.CompareTag("Box"))
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }


        }
    }
}

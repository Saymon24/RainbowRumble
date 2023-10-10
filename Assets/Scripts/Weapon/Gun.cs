using RayFire;
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
    [SerializeField] float bulletSpeed = 10f;
    [SerializeField] GameObject Bullet;

    [SerializeField] private RayfireGun rayfireGun;
    [SerializeField] private Transform rayFireGunTarget;

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
        GameObject createdBullet = Instantiate(Bullet, gunEnd.position, gunEnd.rotation);
        RaycastHit hit;
        
        createdBullet.GetComponent<Rigidbody>().velocity = transform.TransformDirection(new Vector3(0, 0, bulletSpeed));
        muzzleFlash.GetComponent<ParticleSystem>().Play();

        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            rayFireGunTarget.position = hit.point;
            rayfireGun.Shoot();

            if (hit.transform.gameObject.CompareTag("Box"))
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }
        }
    }
}

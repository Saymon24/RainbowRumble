using RayFire;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterGun : MonoBehaviour
{

    [Header("Gun Settings")]
    [SerializeField] GunData gunData;
    [SerializeField] Transform gunEnd;
    [SerializeField] GameObject muzzleFlash;
    [SerializeField] GameObject impactEffect;
    [SerializeField] LineRenderer beam;

    [SerializeField] private RayfireGun rayfireGun;
    [SerializeField] private Transform rayFireGunTarget;

    [SerializeField] Camera fpsCam;

    private float timeSinceLastShoot = 0f;

    private void Awake()
    {
        beam.enabled = false;
    }

    private void Activate()
    {
        beam.enabled = true;
    }

    private void DeActivate() { 
        beam.enabled = false;
    }

    public void StartReload()
    {
        if (!gunData.reloading)
        {
            StartCoroutine(Reload());
        }
    }

    private IEnumerator Reload()
    {
        gunData.reloading = true;
        yield return new WaitForSeconds(gunData.reloadTime);
        gunData.currentAmmo = gunData.magSize;
        gunData.reloading = false;
    }

    private bool CanShoot()
    {
        return !gunData.reloading;
    }

    private void PercentageMagasin()
    {
        float waterSentPerShot = gunData.fireRate / 60f;
        gunData.currentAmmo -= waterSentPerShot;

        if (gunData.currentAmmo < 0)
            gunData.currentAmmo = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            if (gunData.currentAmmo > 0)
            {
                if (CanShoot())
                {
                    PercentageMagasin();
                    timeSinceLastShoot = 0;
                    Shoot();
                }
            }

            timeSinceLastShoot += Time.deltaTime;
        }

        if (Input.GetButtonUp("Fire1"))
        {
            DeActivate();
            beam.SetPosition(0, gunEnd.position);
            beam.SetPosition(1, gunEnd.position);
        }

        if (Input.GetButton("Reload"))
        {
            StartReload();
        }

    }

    private void LateUpdate()
    {
        RaycastHit hit;
        Ray ray = new Ray(gunEnd.position, gunEnd.forward);
        bool cast = Physics.Raycast(ray, out hit, gunData.range);
        Vector3 hitPosition = cast ? hit.point : gunEnd.position + gunEnd.forward * gunData.range;

        beam.SetPosition(0, gunEnd.position);
        beam.SetPosition(1, hitPosition);
    }

    private void Shoot()
    {
        RaycastHit hit;
        
        if (muzzleFlash != null)
            muzzleFlash.GetComponent<ParticleSystem>().Play();

        Ray ray = new Ray(gunEnd.position, gunEnd.forward);
        bool cast = Physics.Raycast(ray, out hit, gunData.range);
        Vector3 hitPosition = cast ? hit.point : gunEnd.position + gunEnd.forward * gunData.range;

        beam.SetPosition(0, gunEnd.position);
        beam.SetPosition(1, hitPosition);
        Activate();
    }
}

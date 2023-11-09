using RayFire;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Gun : MonoBehaviour
{

    [Header("Gun Settings")]
    [SerializeField] GunData gunData;
    [SerializeField] Transform gunEnd;
    [SerializeField] GameObject muzzleFlash;
    [SerializeField] GameObject impactEffect;
    [SerializeField] GameObject Bullet;

    [SerializeField] private RayfireGun rayfireGun;
    [SerializeField] private Transform rayFireGunTarget;

    [SerializeField] Camera fpsCam;

    private float timeSinceLastShoot = 0f;

    [Header("Inputs")]
    [SerializeField] private InputActionReference shoot;
    [SerializeField] private InputActionReference reload;

    private void Start()
    {
        gunData.reloading = false;

        if (fpsCam == null)
        {
            fpsCam = GameObject.Find("FPSCamera").GetComponent<Camera>();
        }

    }

    public void StartReload()
    {
        if (!gunData.reloading)
        {
            GameObject.Find("PlayerRig").GetComponent<Animator>().Play("Reload", -1, 0f);
            GetComponentInChildren<Animator>().Play("Reload",-1, 0f);
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
        return !gunData.reloading && timeSinceLastShoot > 1f / (gunData.fireRate / 60f);
    }

    // Update is called once per frame
    void Update()
    {
        GameObject.Find("WeaponSocket").transform.position = GameObject.Find("RightHandSocket").transform.position;
        GameObject.Find("WeaponSocket").transform.rotation = GameObject.Find("RightHandSocket").transform.rotation;

        GameObject.Find("Model").transform.position = GameObject.Find("WeaponSocket").transform.position;
        GameObject.Find("Model").transform.rotation = GameObject.Find("WeaponSocket").transform.rotation;

        if (shoot.action.IsPressed())
        {
            if (gunData.currentAmmo > 0)
            {
                if (CanShoot())
                {
                    gunData.currentAmmo--;
                    timeSinceLastShoot = 0;
                    Shoot();
                }
            }
            timeSinceLastShoot += Time.deltaTime;
        }

        if (reload.action.IsPressed())
        {
            if (gunData.currentAmmo < gunData.magSize)
                StartReload();
        }

    }

    private void Shoot()
    {
        GameObject createdBullet = Instantiate(Bullet, gunEnd.position, gunEnd.rotation);
        float damageMultiplicator = GetComponent<WeaponDatasMultiplicator>().damageMultiplicator;
        RaycastHit hit;
        
        createdBullet.GetComponent<Rigidbody>().velocity = transform.TransformDirection(new Vector3(0, 0, gunData.bulletSpeed));
        muzzleFlash.GetComponent<ParticleSystem>().Play();

        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, gunData.range))
        {
            Instantiate(impactEffect, rayFireGunTarget.position, rayFireGunTarget.rotation);
            rayFireGunTarget.position = hit.point;

            Collider[] overlap = Physics.OverlapSphere(hit.point, 0.1f);

            for (int i = 0; i < overlap.Length; i++)
            {
                if (overlap[i].CompareTag("Enemy"))
                {
                    overlap[i].GetComponent<Enemy>().takeDamage(gunData.damage * damageMultiplicator);
                }
            }
            rayfireGun.Shoot();
        }
    }
}

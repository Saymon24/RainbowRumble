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
    [SerializeField] GameObject beam;
    [SerializeField] Camera fpsCam;

    private bool isActive = false;
    private Vector3 hitPosition = Vector3.zero;
    private GameObject createdBeam;
    private ParticleSystem particleBeam;


    [SerializeField] float gravityMax = 5f;
    float newGravity = 0f;


    private void Awake()
    {
        createdBeam = Instantiate(beam);
        createdBeam.transform.parent = transform;
        createdBeam.GetComponentInChildren<WaterGunParticles>().InitGunData(gunData);

        particleBeam = createdBeam.GetComponentInChildren<ParticleSystem>();
    }

    private void Activate()
    {
        if (isActive)
            return;
        createdBeam.transform.position = gunEnd.position;
        createdBeam.transform.LookAt(hitPosition);
        createdBeam.transform.SetPositionAndRotation((gunEnd.position + hitPosition) / 2, Quaternion.LookRotation(hitPosition - gunEnd.position));
        particleBeam.Play();

        isActive = true;
    }

    private void DeActivate() {

        if (createdBeam != null)
        {
            particleBeam.Stop();
            isActive = false;
        }
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
        newGravity = 0f;
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
        ApplyNewGravityToParticles();
    }

    void ApplyNewGravityToParticles()
    {
        newGravity = gravityMax - gunData.currentAmmo * gravityMax / 100;
        ParticleSystem.MainModule main = particleBeam.main;
        main.gravityModifierMultiplier = newGravity;
    }

    void Update()
    {
        particleBeam.GetComponent<WaterGunParticles>().UpdateDamageMultiplicator(GetComponent<WeaponDatasMultiplicator>().damageMultiplicator);

        if (Input.GetButton("Fire1"))
        {
            if (gunData.currentAmmo > 0)
            {
                if (CanShoot())
                {
                    PercentageMagasin();
                    Shoot();
                }
            }
        }

        if (Input.GetButtonUp("Fire1") || gunData.currentAmmo < 0f)
        {
            DeActivate();
        }

        if (Input.GetButton("Reload"))
        {
            StartReload();
        }

    }

    private void LateUpdate()
    {
        RaycastHit hit;
        Ray ray = fpsCam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        bool cast = Physics.Raycast(ray, out hit, gunData.range);
        hitPosition = cast ? hit.point : ray.GetPoint(gunData.range);

        if (isActive)
        {
            createdBeam.transform.position = (gunEnd.position);
            float distance = Vector3.Distance(gunEnd.position, hitPosition);
            createdBeam.transform.localScale = new Vector3(createdBeam.transform.localScale.x, createdBeam.transform.localScale.y, distance / 2);
            createdBeam.transform.LookAt(hitPosition);
        }
    }

    private void Shoot()
    {
        Activate();
    }

    public GunData GetGunData()
    {
        return gunData;
    }








}

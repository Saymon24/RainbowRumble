using RayFire;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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

    [Header("Inputs")]
    [SerializeField] private InputActionReference shoot;
    [SerializeField] private InputActionReference reload;

    private WaterGunParticles waterP;

    private void Awake()
    {
        createdBeam = Instantiate(beam);
        createdBeam.GetComponentInChildren<WaterGunParticles>().InitGunData(gunData);

        particleBeam = createdBeam.GetComponentInChildren<ParticleSystem>();

        if (fpsCam == null)
        {
            fpsCam = GameObject.Find("FPSCamera").GetComponent<Camera>();
        }

    }

    private void Start()
    {
        waterP = particleBeam.GetComponent<WaterGunParticles>();
    }

    private void Activate()
    {
        if (isActive)
            return;

        if (gunData.currentAmmo <= 0)
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
        waterP.UpdateDamageMultiplicator(GetComponent<WeaponDatasMultiplicator>().damageMultiplicator);

        if (shoot.action.IsPressed())
        {
            if (gunData.currentAmmo > 0)
            {
                if (CanShoot())
                {

                    if (!particleBeam.isPlaying)
                    {
                        particleBeam.Play();
                    }

                    PercentageMagasin();
                    Shoot();
                }
            }
        }

        if (!shoot.action.IsPressed() || gunData.currentAmmo <= 0f)
        {
            DeActivate();
        }

        if (reload.action.IsPressed())
        {
            if (gunData.currentAmmo < gunData.magSize)
                StartReload();
        }

    }

    private void OnDisable()
    {
        if (particleBeam)
            particleBeam.Stop();
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

    public void DestroyBeam()
    {
        Destroy(beam);
    }






}

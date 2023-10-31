using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Gun", menuName = "Weapon/Gun")]
public class GunData : ScriptableObject
{

    [Header("Info")]
    public new string name;
    public WeaponType type;

    [Header("Shooting")]
    public float damage;
    public float range;
    public float bulletSpeed;
    public float impactForce;

    [Header("Reloading")]
    public float currentAmmo;
    public int magSize;
    [Tooltip("In RPM")] public float fireRate;
    public float reloadTime;
    [HideInInspector] public bool reloading;

    [Header("Throwable")]
    public float throwForce;
    public float throwUpwardForce;
    public float throwCooldown;
    public float timeBeforeExplosion;
    public bool expodeOnImpact;

}
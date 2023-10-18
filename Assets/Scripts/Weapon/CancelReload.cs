using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CancelReload : MonoBehaviour
{
    [SerializeField] private GunData currentWeaponData;

    public void CancelReloadOnWeapon()
    {
        currentWeaponData.reloading = false;
    }
}

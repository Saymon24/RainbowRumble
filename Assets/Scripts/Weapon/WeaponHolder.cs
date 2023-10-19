using System;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHolder : MonoBehaviour
{

    private List<GameObject> weapons;
    private bool selectedWeapon = false;
    private bool previousSelectedWeapon = true; 

    // Start is called before the first frame update
    void Start()
    {
        weapons = new List<GameObject>();

        foreach (Transform weapon in transform)
        {
            print(weapon.gameObject.name);
            weapons.Add(weapon.gameObject);
        }
        SelectWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Switch"))
        {
            previousSelectedWeapon = selectedWeapon;
            selectedWeapon = !selectedWeapon;
        }

        if (previousSelectedWeapon != selectedWeapon)
        {
            SelectWeapon();
        }
    }

    private void SelectWeapon()
    {
        int i = 0;
        foreach (Transform weapon in transform)
        {
            if (i == Convert.ToInt32(selectedWeapon))
            {
                weapon.gameObject.SetActive(true);
            }
            else
            {
                weapon.gameObject.GetComponent<CancelReload>().CancelReloadOnWeapon();
                weapon.gameObject.SetActive(false);
            }
            i++;
        }
    }

    public GameObject GetSelectedWeapon()
    {
        return weapons[Convert.ToInt32(selectedWeapon)];
    }

    public GunData GetWeaponData()
    {
        return weapons[Convert.ToInt32(selectedWeapon)].GetComponent<GetGunData>().GetData();
    }

}

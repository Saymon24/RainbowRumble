using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponHolder : MonoBehaviour
{

    private List<GameObject> weapons;
    private bool selectedWeapon = false;
    private bool previousSelectedWeapon = true;

    [Header("Inputs")]
    [SerializeField] private InputActionReference changeWeapon;

    // Start is called before the first frame update
    void Start()
    {
        weapons = new List<GameObject>();

        foreach (Transform weapon in transform)
        {
            weapons.Add(weapon.gameObject);
        }
        SelectWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        if (changeWeapon.action.WasPerformedThisFrame())
        {
            if (weapons.Count != 2)
                return;
            previousSelectedWeapon = selectedWeapon;
            selectedWeapon = !selectedWeapon;
            if (!GameObject.Find("Player").GetComponent<CandyEffects>())
            {
                weapons[0].GetComponent<WeaponDatasMultiplicator>().damageMultiplicator = 1f;
                weapons[1].GetComponent<WeaponDatasMultiplicator>().damageMultiplicator = 1f;
            }
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
                GameObject handSocket = GameObject.Find("RightHandSocket");
                Transform weaponSocket = weapon.Find("WeaponSocket");
                Transform weaponModel = weapon.Find("Model");

                weapon.position = handSocket.transform.position;
                weaponModel.position = weaponSocket.position;
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

    public GunData GetWeaponDataFromIndex(int index)
    {
        if (index > weapons.Count - 1)
        {
            return null;
        }

        return weapons[index].GetComponent<GetGunData>().GetData();
    }

    public void AddWeapon(GameObject weapon, GunData wData)
    {
        bool alreadyOwn = false;

        int index = 0;
        foreach (GameObject w in weapons)
        {

            GunData tmp = GetWeaponDataFromIndex(index);

            if (tmp.name == wData.name)
            {
                GetWeaponData().currentAmmo = GetWeaponData().magSize;
                alreadyOwn = true;
            }
            index++;
        }

        if (alreadyOwn)
            return;

        if (weapons.Count != 1)
        {
            GameObject currentW = GetSelectedWeapon();
            int currentIndex = Convert.ToInt32(selectedWeapon);

            weapons.Remove(currentW);

            if (currentW.GetComponent<GetGunData>().GetData().name == "WaterGun")
            {
                currentW.GetComponent<WaterGun>().DestroyBeam();
            }

            Destroy(currentW);
        }

        GameObject socket = GameObject.Find("RightHandSocket");
        GameObject newWeapon = Instantiate(weapon, socket.transform.position, socket.transform.rotation);
        weapons.Add(newWeapon);
        newWeapon.transform.parent = this.transform;

        Transform[] allChildren = GetComponentsInChildren<Transform>();

        // Parcourez tous les objets et changez leur layer.
        foreach (Transform child in allChildren)
        {
            child.gameObject.layer = LayerMask.NameToLayer("FPSLayer");
        }
        newWeapon.layer = LayerMask.NameToLayer("FPSLayer");
    }

}

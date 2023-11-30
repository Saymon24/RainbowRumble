using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

[System.Serializable]
public class ShopWeapon
{

    public string _weaponName;
    public List<WeaponsUpgrades> _allUpgrades;

    //public List<string> skins;

    public ShopWeapon(string name, List<WeaponsUpgrades> upgrades)
    {
        _weaponName = name;
        _allUpgrades = upgrades;
    }

    public void PrintWeapon()
    {
        Debug.Log("---------" + _weaponName);

        foreach(var upgrade in _allUpgrades) {
            upgrade.PrintInfos();
        }
    }
}

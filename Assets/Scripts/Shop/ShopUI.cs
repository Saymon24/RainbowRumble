using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{

    [SerializeField] private Button Damage;
    [SerializeField] private Button FireRate;
    [SerializeField] private Button MagSize;

    private string weaponToDisplay = "Narf";
    private SaveManager saveManager;
    
    private int damageUpgradeCost;
    private int fireRateUpgradeCost;
    private int magSizeUpgradeCost;

    private int damageMaxlevel;
    private int fireRateMaxlevel;
    private int magSizeMaxlevel;

    private int damageCurrentLevel;
    private int fireRateCurrentLevel;
    private int magSizeCurrentLevel;

    private void Start()
    {

        if (GameObject.Find("SaveManager").TryGetComponent(out SaveManager saveM))
        {

            saveManager = saveM;
            ChangeUpgradeDisplayed();

        }

    }

    public void ChangeActiveWeapon(string weaponName)
    {

        weaponToDisplay = weaponName;
        ChangeUpgradeDisplayed();

    }

    private void ChangeUpgradeDisplayed()
    {
        List<WeaponsUpgrades> up = saveManager.GetUpgradeByWeaponName(weaponToDisplay);

        if (up == null)
            return;

        foreach (WeaponsUpgrades weapon in up)
        {

            if (weapon == null)
                continue;

            if (weapon._upgradeName == "Damage")
            {
                //damageUpgradeCost = weapon._upgradeCost;
                damageMaxlevel = weapon._maxLevel;
                damageCurrentLevel = weapon._level;
            }
            else if (weapon._upgradeName == "Fire Rate")
            {
                //fireRateUpgradeCost = weapon._upgradeCost;
                fireRateMaxlevel = weapon._maxLevel;
                fireRateCurrentLevel = weapon._level;
            }
            else if (weapon._upgradeName == "Magazine Size")
            {
                //magSizeUpgradeCost = weapon._upgradeCost;
                magSizeMaxlevel = weapon._maxLevel;
                magSizeCurrentLevel = weapon._level;
            }
        }
    }

}

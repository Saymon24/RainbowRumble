using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{

    [SerializeField] private TMP_Text RCText;
    [SerializeField] private TMP_Text TCText;

    [SerializeField] private Button Damage;
    [SerializeField] private Button FireRate;
    [SerializeField] private Button MagSize;

    [SerializeField] private TMP_Text damageLevelText;
    [SerializeField] private TMP_Text FireRateLevelText;
    [SerializeField] private TMP_Text MagSizeLevelText;

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

    private void OnEnable()
    {
        if (GameObject.Find("SaveManager").TryGetComponent(out SaveManager saveM))
        {

            saveManager = saveM;
            ChangeUpgradeDisplayed();

        }

        RCText.text = "RC: " + saveManager.data.profile.getRC().ToString();
        TCText.text = "TC: " + saveManager.data.profile.getTC().ToString();
    }

    public void ChangeActiveWeapon(string weaponName)
    {

        weaponToDisplay = weaponName;
        ChangeUpgradeDisplayed();

    }

    public void ChangeUpgradeDisplayed()
    {
        List<WeaponsUpgrades> up = saveManager.GetUpgradeByWeaponName(weaponToDisplay);

        if (up == null)
            return;

        int i = 0;

        foreach (WeaponsUpgrades weapon in up)
        {

            if (weapon == null)
            {
                if (i == 0)
                {
                    Damage.enabled = false;
                    damageLevelText.enabled = false;
                } else if (i == 1)
                {
                    FireRate.enabled = false;
                    FireRateLevelText.enabled = false;
                } else if (i == 2)
                {
                    MagSize.enabled = false;
                    MagSizeLevelText.enabled = false;
                }
                i++;
                continue;
            }

            if (weapon._upgradeName == "Damage")
            {

                Damage.enabled = true;
                damageLevelText.enabled = true;

                //damageUpgradeCost = weapon._upgradeCost;
                damageMaxlevel = weapon._maxLevel;
                damageCurrentLevel = weapon._level;
                if (damageLevelText != null)
                    damageLevelText.text = "Level " + damageCurrentLevel + "/" + damageMaxlevel;

            }
            else if (weapon._upgradeName == "Fire Rate")
            {

                FireRate.enabled = true;
                FireRateLevelText.enabled = true;

                //fireRateUpgradeCost = weapon._upgradeCost;
                fireRateMaxlevel = weapon._maxLevel;
                fireRateCurrentLevel = weapon._level;
                if (FireRateLevelText != null)
                    FireRateLevelText.text = "Level " + fireRateCurrentLevel + "/" + fireRateMaxlevel;

            }
            else if (weapon._upgradeName == "Magazine Size")
            {

                MagSize.enabled = true;
                MagSizeLevelText.enabled = true;

                //magSizeUpgradeCost = weapon._upgradeCost;
                magSizeMaxlevel = weapon._maxLevel;
                magSizeCurrentLevel = weapon._level;
                if (MagSizeLevelText != null)
                    MagSizeLevelText.text = "Level " + magSizeCurrentLevel + "/" + magSizeMaxlevel;
            }
            i++;
        }
    }

    public void UpdateUpgrade(upgradeType upgradeType)
    {
        foreach (var w in saveManager.data.shop.gunList)
        {
            if (w._weaponName == weaponToDisplay)
            {
                foreach (WeaponsUpgrades up in w._allUpgrades)
                {
                    if (up._upgradeType == upgradeType)
                    {
                        up.LevelUp();
                        saveManager.SaveAllDatas();
                        return;
                    }
                }
            }
        }
    }

}

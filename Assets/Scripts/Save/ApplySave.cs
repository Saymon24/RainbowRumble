using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ApplySave : MonoBehaviour
{
    [SerializeField]
    private GunData[] allDatas;

    private SaveManager saveManager;

    private void Start()
    {
        saveManager = GetComponent<SaveManager>();
    }

    public void ApplySaveOnPlayer()
    {
        foreach (var gunData in allDatas)
        {
            foreach (var savedData in saveManager.data.shop.gunList)
            {
                if (gunData.name == savedData._weaponName)
                {
                    foreach (var savedUpgrade in savedData._allUpgrades)
                    {
                        if (savedUpgrade._upgradeType == upgradeType.DAMAGE)
                            gunData.damage = savedUpgrade._upgradeValue;

                        if (savedUpgrade._upgradeType == upgradeType.FIRE_RATE)
                            gunData.fireRate = savedUpgrade._upgradeValue;

                        if (savedUpgrade._upgradeType == upgradeType.MAGAZINE_SIZE)
                            gunData.magSize = savedUpgrade._upgradeValue.ConvertTo<int>();

                    }
                    break;
                }
            }
        }
    }
}

using UnityEngine;

public class WeaponButton : MonoBehaviour
{



    public void askDisplayToManager(string weaponName)
    {
        if (GameObject.Find("ShopUI").TryGetComponent(out ShopUI shopUI))
        {
            shopUI.DisplayUpgradeByWeaponName(weaponName);
        }
    }

}

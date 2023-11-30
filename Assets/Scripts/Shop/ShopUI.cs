using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{

    [SerializeField] private Button Damage;
    [SerializeField] private Button FireRate;
    [SerializeField] private Button MagSize;

    private string weaponToDisplay = "Narf";

    public void DisplayUpgradeByWeaponName(string weaponName)
    {
        weaponToDisplay = weaponName;
    }

    private void ChangeUpgradeDisplayed()
    {
        return;
    }

}

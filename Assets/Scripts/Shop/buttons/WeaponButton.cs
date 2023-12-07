using UnityEngine;

public class WeaponButton : MonoBehaviour
{

    [SerializeField] private string WeaponName;

    private ShopUI shopUI;

    private void Start()
    {
        if (GameObject.Find("ShopMenu").TryGetComponent(out ShopUI shopU))
        {
            shopUI = shopU;
        }
    }

    public void ChangeActiveWeapon()
    {

        shopUI.ChangeActiveWeapon(WeaponName);

    }


    /*    public void askDisplayToManager(string weaponName)
        {
            if (GameObject.Find("ShopUI").TryGetComponent(out ShopUI shopUI))
            {
                shopUI.DisplayUpgradeByWeaponName(weaponName);
            }
        }*/




}

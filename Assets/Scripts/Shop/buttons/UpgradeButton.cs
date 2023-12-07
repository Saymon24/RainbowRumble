using UnityEngine;

public class UpgradeButton : MonoBehaviour
{

    [SerializeField] private upgradeType upgradeType;

    private ShopUI shop;

    private void Start()
    {
        if (GameObject.Find("ShopMenu").TryGetComponent(out ShopUI shopUI))
        {

            shop = shopUI;

        }
    }

    public void BuyUpgrade()
    {
        shop.UpdateUpgrade(upgradeType);
        shop.ChangeUpgradeDisplayed();
    }

}

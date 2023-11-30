
using System.Collections.Generic;

[System.Serializable]
public class ShopData
{
    public List<ShopWeapon> gunList;
    public ShopData() {}

    public void CreateShopData()
    {
        gunList = new List<ShopWeapon>();

        List<WeaponsUpgrades> narfUpgrades = new List<WeaponsUpgrades>
        {
            // Narf
            new WeaponsUpgrades(upgradeType.DAMAGE, "Damage", 0, 5, new[] { 50.0f, 60.0f, 70.0f, 80f, 90f, 100f }),
            new WeaponsUpgrades(upgradeType.FIRE_RATE, "Fire Rate", 0, 4, new[] { 550.0f, 600.0f, 620.0f, 680f, 700f }),
            new WeaponsUpgrades(upgradeType.MAGAZINE_SIZE, "Magazine Size", 0, 2, new[] { 30f, 35f, 50f }),
        };

        gunList.Add(new ShopWeapon("Narf", narfUpgrades));

        List<WeaponsUpgrades> waterGunUpgrades = new List<WeaponsUpgrades>
        {
            // WaterGun
            new WeaponsUpgrades(upgradeType.DAMAGE, "Damage", 0, 2, new[] { 2.0f, 2.5f, 3f }),
        };

        gunList.Add(new ShopWeapon("WaterGun", waterGunUpgrades));
    }

    public void LoadShopData()
    {
        // Narf
        gunList[0]._allUpgrades[0].UpdatePrivateInformations(5, new[] { 50.0f, 60.0f, 70.0f, 80f, 90f, 100f });
        gunList[0]._allUpgrades[1].UpdatePrivateInformations(4, new[] { 550.0f, 600.0f, 620.0f, 680f, 700f });
        gunList[0]._allUpgrades[2].UpdatePrivateInformations(2, new[] { 30f, 35f, 50f });

        // WaterGun
        gunList[1]._allUpgrades[0].UpdatePrivateInformations(2, new[] { 2.0f, 2.5f, 3f });
    }

    public void PrintAllShop()
    {
        foreach (ShopWeapon weapon in gunList)
            weapon.PrintWeapon();
    }
}

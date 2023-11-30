
using System.Collections.Generic;

[System.Serializable]
public class SaveData
{
    public ShopData shop;
    public ProfileData profile;

    public SaveData() {
        shop = new ShopData();
        profile = new ProfileData();
    }

    public void CreateAllData()
    {
        shop.CreateShopData();
        profile.CreateProfileData();
    }

    public void PrintAllData()
    {
        shop.PrintAllShop();
        profile.PrintAllProfile();
    }
}

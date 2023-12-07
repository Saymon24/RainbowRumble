
using UnityEngine;
using System.IO;
using System.Collections.Generic;

public class SaveManager : MonoBehaviour
{

    static public SaveManager instance;
    public SaveData data;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        data = new SaveData();
        LoadSaveData();
    }

    public void SaveAllDatas()
    {
        string json = JsonUtility.ToJson(data);
        using StreamWriter writer = new StreamWriter(Application.persistentDataPath + "/SaveData.json");
        writer.Write(json);

    }

    private void LoadSaveData()
    {
        string filePath = Application.persistentDataPath + "/SaveData.json";

        if (File.Exists(filePath))
        {
            using StreamReader reader = new StreamReader(filePath);
            string json = reader.ReadToEnd();

            data = JsonUtility.FromJson<SaveData>(json);

            data.shop.LoadShopData();
            data.profile.LoadProfileData();
        }
        else
        {
            data.CreateAllData();
            SaveAllDatas();
        }
    }

    public List<WeaponsUpgrades> GetUpgradeByWeaponName(string weaponName)
    {

        List<WeaponsUpgrades> upgradeList = new List<WeaponsUpgrades>
        {
            data.shop.GetWeaponsUpgrades(weaponName, upgradeType.DAMAGE),
            data.shop.GetWeaponsUpgrades(weaponName, upgradeType.FIRE_RATE),
            data.shop.GetWeaponsUpgrades(weaponName, upgradeType.MAGAZINE_SIZE)
        };

        return upgradeList;

    }

}

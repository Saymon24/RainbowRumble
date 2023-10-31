using UnityEngine;
using System.IO;

public class ShopLogic : MonoBehaviour
{

    private SaveData data;

    // Start is called before the first frame update
    void Start()
    {
        string filePath = Application.persistentDataPath + "/SaveData.json";

        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            data = JsonUtility.FromJson<SaveData>(json);
        }

        /*if (SaveData.gunList.Count == 0)
        {
            GenerateShopData();
        }*/

    }

    public void LoadShopData()
    {
        return;
    }

    public void SaveShopData()
    {
        return;
    }

    private void GenerateShopData()
    {

        ShopWeapon w = new ShopWeapon();

        w.weaponName = "Narf";
        w.upgradesLevel.Add(0);

    }

}

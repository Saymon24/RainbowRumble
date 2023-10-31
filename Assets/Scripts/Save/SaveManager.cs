
using UnityEngine;
using System.IO;

public class SaveManager : MonoBehaviour
{

    private SaveData data;

    private void Awake()
    {
        LoadSaveData();
    }

    private void LoadSaveData()
    {
        string filePath = Application.persistentDataPath + "/SaveData.json";

        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            data = JsonUtility.FromJson<SaveData>(json);
        }

    }
}

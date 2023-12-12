using UnityEngine;

[System.Serializable]
public class ProfileData
{
    [SerializeField] private int rainbowCoins;
    [SerializeField] private int teddyCoins;

    public ProfileData() { }

    public void CreateProfileData()
    {
        rainbowCoins = 0;
        teddyCoins = 0;
    }

    public void LoadProfileData()
    {
        PrintAllProfile();
    }

    public void PrintAllProfile()
    {
        Debug.Log("RC " + rainbowCoins);
        Debug.Log("TC " + teddyCoins);
    }

    public void AddCoins(int RC, int TD)
    {
        rainbowCoins += RC;
        teddyCoins += TD;
    }

    public int getRC()
    {
        return rainbowCoins;
    }

    public int getTC()
    {
        return teddyCoins;
    }

}

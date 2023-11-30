using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ProfileData
{
    public int rainbowCoins;
    public int teddyCoins;

    public ProfileData() { }

    public void CreateProfileData()
    {
        rainbowCoins = 0;
        teddyCoins = 0;
    }

    public void LoadProfileData()
    {

    }

    public void PrintAllProfile()
    {
        Debug.Log("RC" + rainbowCoins);
        Debug.Log("TC" + teddyCoins);
    }
}

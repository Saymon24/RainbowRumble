using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RarityPowerUp : MonoBehaviour
{
    private int rarity = 0;

    public void setRarity(int newRarity)
    {
        rarity = newRarity;

        GetComponentInChildren<RarityBonus>().setRarity(newRarity);
    }

    public int getRarity()
    {
        return rarity;
    }
}

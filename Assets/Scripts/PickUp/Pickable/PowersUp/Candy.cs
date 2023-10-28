using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candy : MonoBehaviour
{
    public void giveEffects()
    {
        if (GameObject.Find("WeaponHolder").GetComponent<CandyEffects>())
        {
            CandyEffects effect = GameObject.Find("WeaponHolder").GetComponent<CandyEffects>();

            if (effect.getRarity() <= GetComponent<RarityPowerUp>().getRarity())
            {
                CandyEffects newEffect = GameObject.Find("WeaponHolder").AddComponent<CandyEffects>();
                newEffect.setNewRarity(GetComponent<RarityPowerUp>().getRarity());

                // Destroy old instance
                Destroy(effect);
            }
            else
            {
                effect.ResetTime();
            }
        }
        else
        {
            CandyEffects effect = GameObject.Find("WeaponHolder").AddComponent<CandyEffects>();
            effect.setNewRarity(GetComponent<RarityPowerUp>().getRarity());
        }
    }
}

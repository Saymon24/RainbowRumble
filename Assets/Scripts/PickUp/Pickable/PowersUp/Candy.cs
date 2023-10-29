using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candy : MonoBehaviour
{
    public void giveEffects()
    {
        if (GameObject.Find("Player").GetComponent<CandyEffects>())
        {
            CandyEffects effect = GameObject.Find("Player").GetComponent<CandyEffects>();

            if (effect.getRarity() <= GetComponent<RarityPowerUp>().getRarity())
            {
                CandyEffects newEffect = GameObject.Find("Player").AddComponent<CandyEffects>();
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
            CandyEffects effect = GameObject.Find("Player").AddComponent<CandyEffects>();
            effect.setNewRarity(GetComponent<RarityPowerUp>().getRarity());
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sugar : MonoBehaviour
{
    public void giveEffects()
    {
        if (GameObject.Find("Player").GetComponent<SpeedUpEffects>())
        {
            SpeedUpEffects effect = GameObject.Find("Player").GetComponent<SpeedUpEffects>();

            if (effect.getRarity() <= GetComponent<RarityPowerUp>().getRarity())
            {
                SpeedUpEffects newEffect = GameObject.Find("Player").AddComponent<SpeedUpEffects>();
                newEffect.setNewRarity(GetComponent<RarityPowerUp>().getRarity());

                // Destroy old instance
                Destroy(effect);
            } else
            {
                effect.ResetTime();
            }
        } else
        {
            SpeedUpEffects effect = GameObject.Find("Player").AddComponent<SpeedUpEffects>();
            effect.setNewRarity(GetComponent<RarityPowerUp>().getRarity());
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeBiberon : MonoBehaviour
{
    public void giveEffects()
    {
        BiberonEffects effect = GameObject.Find("Player").AddComponent<BiberonEffects>();
        effect.setNewRarity(GetComponent<RarityPowerUp>().getRarity());
        effect.applyEffect();
    }
}

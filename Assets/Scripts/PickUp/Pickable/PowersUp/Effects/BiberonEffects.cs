using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiberonEffects : MonoBehaviour
{
    private float[] effectMultiplicator = { 1.2f, 1.3f, 1.4f, 1.7f, 2.0f};
    private int rarity = 0;

    public void applyEffect()
    {
        if (GetComponent<HealthController>()._currentPlayerHealth * effectMultiplicator[rarity] > GetComponent<HealthController>().maxPlayerHealth)
            GetComponent<HealthController>()._currentPlayerHealth = GetComponent<HealthController>().maxPlayerHealth;
        else
            GetComponent<HealthController>()._currentPlayerHealth = GetComponent<HealthController>()._currentPlayerHealth * effectMultiplicator[rarity];
        Destroy(this.GetComponent<BiberonEffects>());
    }

    public void setNewRarity(int rarityIndex)
    {
        if (rarityIndex < 0 || rarityIndex >= effectMultiplicator.Length)
            return;

        rarity = rarityIndex;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiberonEffects : MonoBehaviour
{
    private float[] effectMultiplicator = { 1.2f, 1.3f, 1.4f, 1.7f, 2.0f};
    private int rarity = 0;

    public void applyEffect()
    {
        if (GetComponent<PlayerManager>().health * effectMultiplicator[rarity] > GetComponent<PlayerManager>().maxHealth)
            GetComponent<PlayerManager>().health = GetComponent<PlayerManager>().maxHealth;
        else
            GetComponent<PlayerManager>().health = GetComponent<PlayerManager>().health * effectMultiplicator[rarity];
        Destroy(this.GetComponent<BiberonEffects>());
    }

    public void setNewRarity(int rarityIndex)
    {
        if (rarityIndex < 0 || rarityIndex >= effectMultiplicator.Length)
            return;

        rarity = rarityIndex;
    }
}

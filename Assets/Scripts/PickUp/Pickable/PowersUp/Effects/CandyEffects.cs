using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandyEffects : MonoBehaviour
{
    [SerializeField] private float[] speedMultiplicator = { 1.2f, 1.4f, 1.6f, 1.8f, 2.0f };
    [SerializeField] private int rarity = 0;
    [SerializeField] private float durationEffect = 30.0f;

    private float startTime = 0f;

    private void Awake()
    {
        startTime = Time.time;

    }

    public void setNewRarity(int rarityIndex)
    {
        if (rarityIndex < 0 || rarityIndex >= speedMultiplicator.Length)
            return;

        rarity = rarityIndex;
        GetComponent<PlayerMovement>().speedMultiplicator = speedMultiplicator[rarityIndex];
    }

    public int getRarity() { return rarity;}

    public void ResetTime()
    {
        startTime = Time.time;
    }

    private void Update()
    {
        float elapsedTime = Time.time - startTime;

        if (elapsedTime > durationEffect)
        {
            GetComponent<PlayerMovement>().speedMultiplicator = 1f;
            Destroy(this.GetComponent<SpeedUpEffects>());
        }
    }
}

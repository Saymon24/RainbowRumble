using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandyEffects : MonoBehaviour
{
    [SerializeField] private float[] effectMultiplicator = { 1.2f, 1.4f, 1.6f, 1.8f, 2.0f };
    [SerializeField] private int rarity = 0;
    [SerializeField] private float durationEffect = 10.0f;

    private GameObject weaponHolder;

    private float startTime = 0f;

    private void Awake()
    {
        startTime = Time.time;
        weaponHolder = GameObject.Find("WeaponHolder");
    }

    public void setNewRarity(int rarityIndex)
    {
        if (rarityIndex < 0 || rarityIndex >= effectMultiplicator.Length)
            return;

        rarity = rarityIndex;
    }

    public int getRarity() { return rarity;}

    public void ResetTime()
    {
        startTime = Time.time;
    }

    private void Update()
    {
        float elapsedTime = Time.time - startTime;

        foreach (WeaponDatasMultiplicator component in weaponHolder.GetComponent<WeaponHolder>().GetComponentsInChildren<WeaponDatasMultiplicator>())
            component.damageMultiplicator = effectMultiplicator[rarity];

        if (elapsedTime > durationEffect)
        {
            foreach (WeaponDatasMultiplicator component in weaponHolder.GetComponent<WeaponHolder>().GetComponentsInChildren<WeaponDatasMultiplicator>())
                component.damageMultiplicator = 1f;
            Destroy(this.GetComponent<CandyEffects>());
        }
    }
}

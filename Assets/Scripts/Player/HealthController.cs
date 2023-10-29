using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    [Header("Player Health Amount")]
    public float currentPlayerHealth = 100.0f;
    [SerializeField] private float maxPlayerHealth = 100.0f;
    [SerializeField] private int regenRate = 1;
    private bool canRegen = false;

    [Header("Splatter Image")]
    [SerializeField] private Image blackSplatterImage = null;

    [Header("Hurt Image")]
    [SerializeField] private Image hurtImage = null;
    [SerializeField] private float hurtTimer = 0.1f;

    [Header("Heal Timer")]
    [SerializeField] private float healCooldown = 3.0f;
    [SerializeField] private float maxHealCouldown = 3.0f;
    [SerializeField] private bool startCooldown = false;

    void UpdateHealth()
    {
        Color splatterAlpha = blackSplatterImage.color;
        splatterAlpha.a = 1 - (currentPlayerHealth / maxPlayerHealth);
        blackSplatterImage.color = splatterAlpha;
    }

    IEnumerator HurtFlash()
    {
        hurtImage.enabled = true;
        yield return new WaitForSeconds(1);
        hurtImage.enabled = false;
    }

    public void TakeDamage()
    {
        if (currentPlayerHealth >= 0)
        {
            canRegen = false;
            StartCoroutine(HurtFlash());
            UpdateHealth();
            healCooldown = maxHealCouldown;
            startCooldown = true;
        }
    }

    private void Update()
    {
        if (startCooldown)
        {
            healCooldown -= Time.deltaTime;
            if (healCooldown <= 0)
            {
                canRegen = true;
                startCooldown = false;
            }
            if (canRegen)
            {
                if (currentPlayerHealth <= maxPlayerHealth - 0.01)
                {
                    currentPlayerHealth += Time.deltaTime * regenRate;
                    UpdateHealth();
                } else
                {
                    currentPlayerHealth = maxPlayerHealth;
                    healCooldown = maxHealCouldown;
                    canRegen = false;
                }
            }
        }
    }
}

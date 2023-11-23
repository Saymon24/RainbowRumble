using EZCameraShake;
using Microsoft.Win32.SafeHandles;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    [Header("Player Health Amount")]
    [SerializeField] public float maxPlayerHealth = 100.0f;
    public float _currentPlayerHealth = 100.0f;
    [SerializeField] private int regenRate = 1;
    [SerializeField] private int regenHurtRate = 1;
    private bool canRegen = false;
    private bool canHurtRegen = false;

    [Header("Splatter Image")]
    [SerializeField] private Image blackSplatterImage = null;

    [Header("Hurt Image")]
    [SerializeField] private Image hurtImage = null;
    [SerializeField] private Image hurtBloodImage = null;

    [Header("Heal Timer")]
    [SerializeField] private float healCooldown = 3.0f;
    [SerializeField] private float maxHealCouldown = 3.0f;
    [SerializeField] private bool startCooldown = false;

    void UpdateHealth()
    {
        Color splatterAlpha = blackSplatterImage.color;
        Color bloodAlpha = hurtBloodImage.color;
        splatterAlpha.a = 1 - (_currentPlayerHealth / maxPlayerHealth);
        bloodAlpha.a = 1 - (_currentPlayerHealth / maxPlayerHealth);
        blackSplatterImage.color = splatterAlpha;
        hurtBloodImage.color = bloodAlpha;
    }

    void HurtFlash(float damage)
    {
        Color hurtSplatter = hurtImage.color;
        hurtSplatter.a = damage * 100f / _currentPlayerHealth / 100;
        hurtImage.color = hurtSplatter;
        canHurtRegen = true;
    }

    private void Die()
    {
         if (GameObject.Find("DeathUI").TryGetComponent(out DeathMenu death))
         {
            death.DeathScreen();
            AudioManager.instance.PlayMusic("Death");
         }
    }

    public void TakeDamage(float damage)
    {
        CameraShaker.Instance.ShakeOnce(2f, 7f, 0.5f, 0.5f);
        if (_currentPlayerHealth - damage >= 0)
        {
            HurtFlash(damage);
            _currentPlayerHealth -= damage;
            UpdateHealth();
            canRegen = false;
            healCooldown = maxHealCouldown;
            startCooldown = true;
        }
        else
            Die();
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
        }

        if (canRegen)
        {
            if (_currentPlayerHealth <= maxPlayerHealth - 0.01)
            {
                _currentPlayerHealth += Time.deltaTime * regenRate;
                UpdateHealth();
            } else
            {
                _currentPlayerHealth = maxPlayerHealth;
                healCooldown = maxHealCouldown;
                canRegen = false;
            }
        }
        if (canHurtRegen)
        {
            if (hurtImage.color.a > 0)
            {
                Color hurtSplatter = hurtImage.color;
                hurtSplatter.a -= Time.deltaTime * regenHurtRate;
                hurtImage.color = hurtSplatter;
            } else
                canHurtRegen = false;
        }
    }
}

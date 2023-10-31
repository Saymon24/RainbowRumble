using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] private float bombDamage = 10f;
    [SerializeField] private HealthController _healthController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _healthController.TakeDamage(bombDamage);
            FindAnyObjectByType<CombatManager>().ActivateCombatMode();
            AudioManager.instance.PlaySFX("Bomb");
        }
    }
}

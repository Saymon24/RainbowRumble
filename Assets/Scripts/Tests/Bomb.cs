using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] private float bombDamage = 10.0f;
    [SerializeField] private HealthController _healthController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _healthController.currentPlayerHealth -= bombDamage;
            _healthController.TakeDamage();
            gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private bool isTrigger = false;
    private GameObject player;

    private void Start()
    {
        player = GameObject.Find("Player");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isTrigger = false;
        }
    }

    public void AttackPlayer()
    {
        if (isTrigger)
        {
            if (player.TryGetComponent(out HealthController healthController))
            {
                GameObject parent = transform.parent.gameObject;

                if (parent.TryGetComponent(out Enemy enemy))
                {
                    healthController.TakeDamage(enemy.damage);
                }
            }
        }

    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    private NavMeshAgent agent;
    private GameObject player;
    public LayerMask groundLayer;

    [Header("Enemy Settings")]
    public float health = 100f;
    public float speed = 10f;
    public float damage = 5f;
    public float spawnRate = 1f; // 1f -> Entity must spawn

    [System.Serializable]
    public class DroppablePowerUp
    {
        public GameObject powerUp;
        public float spawnRate;
    }

    public DroppablePowerUp[] droppablePowerUp;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
        player = GameObject.Find("Player");
        //groundLayer = LayerMask.NameToLayer("Ground");
    }

    void Update()
    {
        if (health <= 0)
            destroyEnemy();
    }

    public void takeDamage(float damage)
    {
        health -= damage;
    }

    public void AttackPlayer()
    {
        player.GetComponent<PlayerManager>().takeDamage(damage);
    }

    private void SpawnPowerUp()
    {
        if (droppablePowerUp.Length == 0)
            return;

        RaycastHit hit;
        Vector3 groundPos = transform.position;

        if (Physics.Raycast(transform.position, Vector3.down, out hit, float.PositiveInfinity, groundLayer))
        {
            groundPos = hit.point;
            Debug.Log("OUI");
        }
        else
        {
            Debug.Log("NON");
            return;
        }

        float randomValue = UnityEngine.Random.Range(0.0f, 1.0f);
        float cumulativeRate = 0;

        for (int i = 0; i < droppablePowerUp.Length; i++)
        {
            float spawnRate = 0;

            spawnRate = droppablePowerUp[i].spawnRate;

            cumulativeRate += spawnRate;

            if (randomValue <= cumulativeRate)
            {
                Instantiate(droppablePowerUp[i].powerUp, groundPos, Quaternion.identity);
                break;
            }
        }
    }

    private void destroyEnemy()
    {
        SpawnPowerUp();
        Destroy(gameObject);
    }
}

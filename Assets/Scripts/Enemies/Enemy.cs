using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private NavMeshAgent agent;
    private GameObject player;

    [Header("Enemy Settings")]
    public float health = 100f;
    public float speed = 10f;
    public float damage = 5f;
    public float spawnRate = 1f; // 1f -> Entity must spawn 
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
        player = GameObject.Find("Player");
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

    private void destroyEnemy()
    {
        Destroy(gameObject);
    }
}

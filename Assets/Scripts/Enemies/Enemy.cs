using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Settings")]
    public float health = 100f;
    public float speed = 10f;
    public float damage = 5f;
    public float spawnRate = 1f; // 1f -> Entity must spawn 

    void Start()
    {
        
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

    private void destroyEnemy()
    {
        Destroy(gameObject);
    }
}

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
    private Animator animator;
    private Rigidbody rb;
    private Collider collider;

    [Header("Enemy Settings")]
    public float health = 100f;
    public float speed = 10f;
    public float damage = 5f;
    public float spawnRate = 1f; // 1f -> Entity must spawn
    public int score = 15;
    public bool isDead = false;

    private EnemyManager manager;

    [System.Serializable]
    public class DroppablePowerUp
    {
        public GameObject powerUp;
        public float spawnRate;
        public float[] rarityRate;
    }

    public DroppablePowerUp[] droppablePowerUp;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
        player = GameObject.Find("Player");
        animator = GetComponentInChildren<Animator>();
        rb = GetComponentInChildren<Rigidbody>();
        collider = GetComponent<Collider>();

        manager = GameObject.Find("EnemyManager").GetComponent<EnemyManager>();
    }

    void Update()
    {
        if (health <= 0)
            Die();
    }

    public void takeDamage(float damage)
    {
        health -= damage;
    }

    public void AttackPlayer()
    {
        AudioManager.instance.PlaySFX("MonsterGrunt");
    }

    public void GiveScore()
    {
        GameObject.Find("GameManager").GetComponent<ScoreManager>().AddScore(score);
    }

    private int GetRarityPowerUp(int powerUpIndex)
    {
        if (droppablePowerUp.Length <= powerUpIndex)
            return 0;
        if (droppablePowerUp[powerUpIndex].rarityRate.Length == 0)
            return 0;

        float randomValue = Mathf.Round(UnityEngine.Random.Range(0.0f, 1.0f) * 10.0f) * 0.1f;
        float cumulativeRate = 0;

        for (int i = 0; i < droppablePowerUp[powerUpIndex].rarityRate.Length; i++)
        {
            float spawnRate = 0;

            spawnRate = droppablePowerUp[powerUpIndex].rarityRate[i];

            cumulativeRate += spawnRate;

            if (randomValue <= cumulativeRate)
                return i;
        }
        return 0;
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
        }
        else
        {
            return;
        }

        float randomValue = Mathf.Round(UnityEngine.Random.Range(0.0f, 1.0f) * 10.0f) * 0.1f;
        float cumulativeRate = 0;

        for (int i = 0; i < droppablePowerUp.Length; i++)
        {
            float spawnRate = 0;

            spawnRate = droppablePowerUp[i].spawnRate;

            cumulativeRate = cumulativeRate + spawnRate;

            if (randomValue <= cumulativeRate)
            {
                GameObject powerUp = Instantiate(droppablePowerUp[i].powerUp, groundPos, Quaternion.identity);  
                int rarityChoosen = GetRarityPowerUp(i);

                powerUp.GetComponent<RarityPowerUp>().setRarity(rarityChoosen);
                break;
            }
        }
    }

    private void Die()
    {
        if (!isDead)
        {
            int randomNum = UnityEngine.Random.Range(1, 4);

            collider.enabled = false;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            agent.enabled = false;
            rb.constraints = RigidbodyConstraints.FreezePosition;

            if (animator)
            {
                animator.SetBool("IsDead", true);
                animator.SetInteger("DeathNumber", randomNum);
            }

            SpawnPowerUp();
            GiveScore();
            manager.RemoveEnemy();

            Invoke(nameof(destroyEnemy), 5);

            isDead = true;
        }
    }

    private void destroyEnemy()
    {
        Destroy(gameObject);
    }
}

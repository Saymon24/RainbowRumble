using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private float health = 100f;
    private bool isAlive = true;

    void Start()
    {
        
    }

    void Update()
    {
        if (health <= 0)
            isAlive = false;

        if (!isAlive)
            Die();
    }

    private void Die()
    {
        GameObject.Find("PlayerCamera").GetComponent<MouseLook>().enabled = false;
        GetComponent<PlayerMovement>().enabled = false;
    }

    public void takeDamage(float damage)
    {
        if (health > 0)
            health -= damage;
        else
            health = 0;
    }
}

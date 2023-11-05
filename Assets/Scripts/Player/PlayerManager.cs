using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public bool isAlive = true;

    void Start()
    {
        
    }

    void Update()
    {
        if (GetComponent<HealthController>()._currentPlayerHealth <= 0)
            Died();
    }

    public void Died()
    {
        isAlive = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUpEffects : MonoBehaviour
{
    [SerializeField] private float speedMultiplicator = 15f;
    [SerializeField] private float durationEffect = 60f;

    private float startTime = 0f;

    private void Start()
    {
        startTime = Time.time;

        GetComponent<PlayerMovement>().speedMultiplicator = speedMultiplicator;
    }

    private void Update()
    {
        float elapsedTime = Time.time - startTime;

        if (elapsedTime > durationEffect)
        {
            GetComponent<PlayerMovement>().speedMultiplicator = 1f;
            Destroy(this.GetComponent<SpeedUpEffects>());
        }
    }
}

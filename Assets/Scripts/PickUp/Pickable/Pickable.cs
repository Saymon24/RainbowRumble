using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Pickable : MonoBehaviour
{
    [SerializeField] float despawnTime = 60.0f;
    private float startTime = 0;
    private bool isPickUp { set; get; } = false;

    [SerializeField] private UnityEvent pickupCallback;

    private void Start()
    {
        startTime = Time.time;
    }

    private void Update()
    {
        float elapsedTime = Time.time - startTime;

        if (elapsedTime >= despawnTime)
            Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPickUp = true;
            pickupCallback.Invoke();
            Destroy(gameObject);
        }
    }
}

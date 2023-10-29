using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Pickable : MonoBehaviour
{
    private bool isPickUp { set; get; } = false;

    [SerializeField] private UnityEvent pickupCallback;

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

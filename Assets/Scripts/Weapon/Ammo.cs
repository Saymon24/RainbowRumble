using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    private void DestroyAmmo()
    {
        if (this)
        {
            Destroy(this.gameObject);
        }
    }

    void Update()
    {
        Invoke(nameof(DestroyAmmo), 1);
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(this.gameObject);
    }
}

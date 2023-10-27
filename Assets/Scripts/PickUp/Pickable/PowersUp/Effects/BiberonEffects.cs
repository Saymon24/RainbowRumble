using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiberonEffects : MonoBehaviour
{
    //[SerializeField] private float effectMultiplicator = 15f;

    private void Update()
    {
        GetComponent<PlayerManager>().health = 120f;
        Destroy(this.GetComponent<BiberonEffects>());
    }
}

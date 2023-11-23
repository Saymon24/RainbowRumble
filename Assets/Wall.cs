using RayFire;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    private void OnDestroy()
    {
        Debug.Log("dsqdssqdsqdq");
        GameObject.Find("GameManager").GetComponent<UpdateNavMeshes>().ForceUpdateNavMeshes();
    }
}

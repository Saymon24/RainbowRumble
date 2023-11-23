using RayFire;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    private void OnDestroy()
    {
        GameObject manager = GameObject.Find("GameManager");

        if (manager == null)
            return;

        if (manager.TryGetComponent(out UpdateNavMeshes navMeshes))
        {
            navMeshes.startReload();
        }
    }
}

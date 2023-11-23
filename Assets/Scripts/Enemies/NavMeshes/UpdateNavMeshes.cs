using System.Collections;
using System.Threading;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class UpdateNavMeshes : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private NavMeshSurface[] surfaces;
    
    public void startReload()
    {
        StartCoroutine(ForceUpdateNavMeshes());
    }
    private IEnumerator ForceUpdateNavMeshes()
    {
        for (int i = 0; i < surfaces.Length; i++)
        {
            if (surfaces[i] != null)
            {
                var async = surfaces[i].UpdateNavMesh(surfaces[i].navMeshData);
                yield return async;
            }
        }
    }
}

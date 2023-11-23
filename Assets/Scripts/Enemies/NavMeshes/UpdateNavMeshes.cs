using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class UpdateNavMeshes : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private bool isUpdate = true;
    [SerializeField] private float NavMeshUpdateTime = 1f;
    [SerializeField] private NavMeshSurface[] surfaces;
    public void ForceUpdateNavMeshes()
    {
        for (int i = 0; i < surfaces.Length; i++)
        {
            if (surfaces[i] != null)
                surfaces[i].BuildNavMesh();
        }
    }
}

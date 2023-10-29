using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class UpdateNavMeshes : MonoBehaviour
{
    private NavMeshSurface[] surfaces;
    private float startTime = 0.0f;

    [Header("Settings")]
    [SerializeField] private bool isUpdate = true;
    [SerializeField] private float NavMeshUpdateTime = 1f;

    // Start is called before the first frame update
    void Start()
    {
        surfaces = GetComponentsInChildren<NavMeshSurface>();
        startTime = Time.time;
    }

    public void ForceUpdateNavMeshes()
    {
        for (int i = 0; i < surfaces.Length; i++)
            surfaces[i].BuildNavMesh();
    }

    // Update is called once per frame
    void Update()
    {
        float elapsedTime = Time.time - startTime;

        if (elapsedTime > NavMeshUpdateTime && isUpdate)
        {
            ForceUpdateNavMeshes();
            startTime = Time.time;
        }
    }
}

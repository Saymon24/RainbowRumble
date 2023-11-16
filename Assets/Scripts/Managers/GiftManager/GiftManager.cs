using System.Collections.Generic;
using UnityEngine;

public class GiftManager : MonoBehaviour
{

    [Header("Spawn")]
    [SerializeField] private List<Transform> GiftSpawnPoints;
    [SerializeField] private GameObject Spawnable;
    [SerializeField] private float SpawnTime;
    [SerializeField] private float ProbabiltySpawn;

    private bool canSpawn = true;
    private float timer = 0f;
    private GameObject SpawnedObject;

    // Start is called before the first frame update
    void Start()
    {
        if (GiftSpawnPoints.Count <= 0)
            canSpawn = false;
        timer = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (!canSpawn)
            return;

        if (SpawnedObject != null)
            timer = Time.time;

        if (Time.time - timer >= SpawnTime)
        {
            timer = Time.time;

            if (Random.Range(0f, 100f) <= ProbabiltySpawn)
            {
                SpawnGift();
            }
        }

    }

    private void SpawnGift()
    {

        if (SpawnedObject != null)
            return;

        int index = Random.Range(0, GiftSpawnPoints.Count);
        SpawnedObject = Instantiate(Spawnable, GiftSpawnPoints[index].position, Quaternion.identity);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnStation : MonoBehaviour
{

    [SerializeField] private GameObject[] weaponsToSpawn;
    [SerializeField] private Transform weaponSpawnPoint;

    private int index;
    // Start is called before the first frame update
    void Start()
    {
       index = Random.Range(0, weaponsToSpawn.Length);
        Instantiate(weaponsToSpawn[index], weaponSpawnPoint.position, Quaternion.identity);
    }

    private void OnTriggerEnter(Collider other)
    {
        print("Trigger Station with: " + weaponsToSpawn[index].name);
    }

}

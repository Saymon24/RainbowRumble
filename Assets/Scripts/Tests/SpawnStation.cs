using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnStation : MonoBehaviour
{

    [SerializeField] private GameObject[] weaponsToSpawn;
    [SerializeField] private Transform weaponSpawnPoint;
    [SerializeField] private bool ChangedWeapon = false;
    [SerializeField] private float timerToChanged = 2f;
    [SerializeField] private bool RotateWeapon = false;

    [Header("Angle per second")]
    [SerializeField] private float rotateSpeed = 180f;

    [Header("Rarity Light Color")]
    [SerializeField] private RarityColor rarityColor;
    [Header("Materials in order of rarity")]
    [SerializeField] private List<Material> rarityMaterial;
    [SerializeField] private bool randomColor;

    private float timer;
    private GameObject currentInstance;
    private Vector3 initialPosition;
    private ParticleSystemRenderer rarityPart;
    private Dictionary<int, Material> colorPair;

    private int index;
    // Start is called before the first frame update
    void Start()
    {

        colorPair = new Dictionary<int, Material>();

        int i = 0;
        
        foreach(Material mat in rarityMaterial)
        {
            colorPair.Add(i, mat);
            i++;
        }

       index = Random.Range(0, weaponsToSpawn.Length);

        if (ChangedWeapon)
        {
            index = 0;
        }

        if (timerToChanged <= 0)
        {
            ChangedWeapon = false;
        }

        currentInstance = Instantiate(weaponsToSpawn[index], weaponSpawnPoint.position, Quaternion.identity);
        initialPosition = currentInstance.transform.position;

        rarityPart = GetComponentInChildren<ParticleSystem>().GetComponent<ParticleSystemRenderer>();
        rarityPart.material = colorPair[(int)rarityColor];
        rarityPart.trailMaterial = colorPair[(int)rarityColor];

    }

    private void Update()
    {

        if (ChangedWeapon)
        {
            if (Time.time - timer > timerToChanged)
            {
                index++;
                Destroy(currentInstance);

                if (index >= weaponsToSpawn.Length)
                    index = 0;

                currentInstance = Instantiate(weaponsToSpawn[index], weaponSpawnPoint.position, Quaternion.identity);

                if (randomColor)
                {
                    int rColor = Random.Range(0, 6);

                    rarityPart.material = colorPair[rColor];
                    rarityPart.trailMaterial = colorPair[rColor];
                }

                timer = Time.time;
            }
        }
    }

    private void FixedUpdate()
    {
        if (RotateWeapon)
        {
            // Rotation
            currentInstance.transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime);

            // Flottement
            float verticalOffset = Mathf.Sin(Time.time * 2f) * 0.1f;
            currentInstance.transform.position = initialPosition + new Vector3(0, verticalOffset, 0);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        print("Trigger Station with: " + weaponsToSpawn[index].name);
    }

}

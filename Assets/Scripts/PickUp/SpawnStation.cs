using System.Collections.Generic;
using UnityEngine;

public class SpawnStation : MonoBehaviour
{
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

    [Header("For loot only")]
    [SerializeField] private Material lootColorPart;
    [SerializeField] private Material lootColorTrail;

    [Header("Dictionnary of weapon")]
    [SerializeField] private List<GameObject> standWeapon;
    [SerializeField] private List<GameObject> usableWeapon;


    public bool debug = false;

    private float timer;
    private GameObject currentInstance;
    private Vector3 initialPosition;
    private ParticleSystemRenderer rarityPart;
    private Dictionary<int, Material> colorPair;
    private Dictionary<GameObject, GameObject> weaponDict;

    private bool hasSpawn = false;

    private int index;
    // Start is called before the first frame update
    void Start()
    {

        if (debug)
            hasSpawn = true;

        colorPair = new Dictionary<int, Material>();

        int i = 0;
        
        foreach(Material mat in rarityMaterial)
        {
            colorPair.Add(i, mat);
            i++;
        }

       index = Random.Range(0, standWeapon.Count);

        if (ChangedWeapon)
        {
            index = 0;
        }

        if (timerToChanged <= 0)
        {
            ChangedWeapon = false;
        }

        currentInstance = Instantiate(standWeapon[index], weaponSpawnPoint.position, Quaternion.identity);
        initialPosition = currentInstance.transform.position;

        rarityPart = GetComponentInChildren<ParticleSystem>().GetComponent<ParticleSystemRenderer>();
        rarityPart.material = colorPair[(int)rarityColor];
        rarityPart.trailMaterial = colorPair[(int)rarityColor];


        if (standWeapon.Count != usableWeapon.Count)
        {
            Debug.Log("ERROR STAND WEAPON != USABLE WEAPON");
        }

        weaponDict = new Dictionary<GameObject, GameObject>();

        for (int k = 0; k != standWeapon.Count; k++)
        {
            weaponDict.Add(standWeapon[k], usableWeapon[k]);
        }

    }

    private void Update()
    {

        if (hasSpawn)
        {
            if (timer >= 30f)
            {
                Destroy(currentInstance);
                Destroy(gameObject);
            }

            rarityPart.material = lootColorPart;
            rarityPart.trailMaterial = lootColorTrail;
            timer += Time.deltaTime;
            return;
        }

        if (ChangedWeapon)
        {
            if (Time.time - timer > timerToChanged)
            {
                index++;
                Destroy(currentInstance);

                if (index >= standWeapon.Count)
                    index = 0;

                currentInstance = Instantiate(standWeapon[index], weaponSpawnPoint.position, Quaternion.identity);

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

        if (other.CompareTag("Player"))
        {

            GunData currentData = currentInstance.GetComponent<GetGunData>().GetData();

            if (currentData.type == WeaponType.Throwable)
            {
                return;
            }
            else
            {
                GameObject w = weaponDict[standWeapon[index]];
                GameObject player = other.gameObject;

                player.GetComponentInChildren<WeaponHolder>().AddWeapon(w, currentData);

            }
            Destroy(gameObject);
            Destroy(currentInstance);
        }

    }

    public void hasBeenSpawn()
    {
        hasSpawn = true;
    }

}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RarityBonus : MonoBehaviour
{
    [SerializeField] private bool RotateWeapon = false;

    [Header("Angle per second")]
    [SerializeField] private float rotateSpeed = 180f;

    [Header("Rarity Light Color")]
    [SerializeField] private RarityColor rarityColor;
    [Header("Materials in order of rarity")]
    [SerializeField] private List<Material> rarityMaterial;

    [SerializeField]  private GameObject currentInstance;
    private Vector3 initialPosition;
    private ParticleSystemRenderer rarityPart;
    private Dictionary<int, Material> colorPair;

    void Awake()
    {
        colorPair = new Dictionary<int, Material>();

        int i = 0;
        
        foreach(Material mat in rarityMaterial)
        {
            colorPair.Add(i, mat);
            i++;
        }

        initialPosition = currentInstance.transform.position;

        rarityPart = GetComponentInChildren<ParticleSystem>().GetComponent<ParticleSystemRenderer>();
        rarityPart.material = colorPair[(int)rarityColor];
        rarityPart.trailMaterial = colorPair[(int)rarityColor];
    }

    public void setRarity(int rarityIndex)
    {
        if (rarityIndex < 0 || rarityIndex >= rarityMaterial.Count)
            return;

        rarityPart.material = rarityMaterial[rarityIndex];
        rarityPart.trailMaterial = rarityMaterial[rarityIndex];
    }

    private void Update()
    {

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
}

using UnityEngine;
using UnityEngine.InputSystem;

public class Gift : MonoBehaviour
{
    [Header("Effects")]
    [SerializeField] private GameObject SmokeEffect;

    [Header("Object to spawn")]
    [SerializeField] private GameObject objectToSpawn;

    [Header("Input")]
    [SerializeField] private InputActionReference open;


    private ParticleSystem crackling;
    private ParticleSystemForceField forceField;

    private GameObject UIInstance;

    private bool canOpenGift = false;

    private Vector3 startingScale;


    private void Start()
    {
        crackling = GetComponentInChildren<ParticleSystem>();
        forceField = GetComponentInChildren<ParticleSystemForceField>();
        startingScale = crackling.transform.localScale;

        UIInstance = GameObject.Find("openGift");

        if (UIInstance != null)
            UIInstance.SetActive(false);
    }

    private void Update()
    {
        if (!canOpenGift)
            return;

        if (open.action.IsPressed())
        {
            canOpenGift = false;
            OpenGift();
        }

    }

    private void OpenGift()
    {
        Debug.Log("Gift opened");
        UIInstance.SetActive(false);

        crackling.transform.localScale = new(10, 10, 10);

        Instantiate(SmokeEffect, transform.position, Quaternion.identity);

        Vector3 spawnPosition = transform.position;
        spawnPosition.y -= 1f;

        GameObject obj = Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);
        obj.GetComponent<SpawnStation>().hasBeenSpawn();

        Transform[] children = GetComponentsInChildren<Transform>();

        foreach (Transform t in children)
        {
            Destroy(t.gameObject);
        }
        Destroy(gameObject);

        return;
    }

    private void OnTriggerEnter(Collider other)
    {
        canOpenGift = true;
        if (UIInstance != null)
            UIInstance.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        canOpenGift = false;
        if (UIInstance != null)
            UIInstance.SetActive(false);
    }

}

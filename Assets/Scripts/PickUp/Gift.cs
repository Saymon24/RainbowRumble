using UnityEngine;

public class Gift : MonoBehaviour
{
    [SerializeField] private GameObject SmokeEffect;


    private ParticleSystem crackling;
    private ParticleSystemForceField forceField;

    private bool canOpenGift = false;

    private void Start()
    {
        crackling = GetComponentInChildren<ParticleSystem>();
        forceField = GetComponentInChildren<ParticleSystemForceField>();
    }

    private void Update()
    {
        if (!canOpenGift)
            return;







    }


    private void OnTriggerEnter(Collider other)
    {
        canOpenGift = true;
    }

    private void OnTriggerExit(Collider other)
    {
        canOpenGift = false;
    }

}

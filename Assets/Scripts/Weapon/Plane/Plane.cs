using RayFire;
using UnityEngine;

public class Plane : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GunData grenadeData;
    [SerializeField] private GameObject explosionParticles;
    private RayfireBomb rayfireBomb;

    private bool hasExploded;

    // Start is called before the first frame update
    void Awake()
    {
        hasExploded = false;
        rayfireBomb = GetComponent<RayfireBomb>();
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (!hasExploded)
        {
            Explode();
            hasExploded = true;
            Destroy(gameObject);
        }
    }


    private void Explode()
    {
        Instantiate(explosionParticles, transform.position, Quaternion.identity);
        Collider[] colliders = Physics.OverlapSphere(transform.position, grenadeData.range);

        foreach (Collider nearbyObject in colliders)
        {

            if (nearbyObject.TryGetComponent<Enemy>(out var enemy))
            {
                enemy.takeDamage(grenadeData.damage);
            }

        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, grenadeData.range);
    }
}

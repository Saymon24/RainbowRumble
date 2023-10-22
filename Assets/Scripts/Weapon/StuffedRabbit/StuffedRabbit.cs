using UnityEngine;
using UnityEngine.UIElements;

public class StuffedRabbit : MonoBehaviour
{

    [Header("References")]
    [SerializeField] private GunData grenadeData;
    [SerializeField] private GameObject explosionParticles;

    private float countDown;
    private bool hasExploded;

    // Start is called before the first frame update
    void Start()
    {
        countDown = grenadeData.timeBeforeExplosion;
        hasExploded = false;
    }

    // Update is called once per frame
    void Update()
    {

        countDown -= Time.deltaTime;

        if (countDown < 0 && !hasExploded)
        {
            Explode();
            hasExploded = true;
        }

    }

    private void Explode()
    {
        Instantiate(explosionParticles, transform.position, Quaternion.identity);

        Collider[] colliders = Physics.OverlapSphere(transform.position, grenadeData.range);

        foreach(Collider nearbyObject in colliders)
        {
            
            if (nearbyObject.TryGetComponent<Enemy>(out var enemy))
            {
                enemy.takeDamage(grenadeData.damage);
            }

        }

        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, grenadeData.range);
    }

}

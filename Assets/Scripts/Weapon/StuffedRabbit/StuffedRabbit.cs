using RayFire;
using UnityEngine;
using UnityEngine.UIElements;

public class StuffedRabbit : MonoBehaviour
{

    [Header("References")]
    [SerializeField] private GunData grenadeData;
    [SerializeField] private GameObject explosionParticles;
    private RayfireBomb rayfireBomb;

    private float countDown;
    private float explosionCountDown = 0.025f;
    private bool hasExploded;

    // Start is called before the first frame update
    void Awake()
    {
        countDown = grenadeData.timeBeforeExplosion;
        hasExploded = false;
        rayfireBomb = GetComponent<RayfireBomb>();
    }

    // Update is called once per frame
    void Update()
    {   
        countDown -= Time.deltaTime;

        if (countDown < 0)
        {
            if (!hasExploded)
            {
                Explode();
                hasExploded = true;
            } else
            {
                explosionCountDown -= Time.deltaTime;

                if (explosionCountDown < 0)
                    Destroy(gameObject);
                else
                    rayfireBomb.Explode(0);
            }
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
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, grenadeData.range);
    }

}

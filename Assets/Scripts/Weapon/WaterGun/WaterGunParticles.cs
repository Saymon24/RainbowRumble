using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterGunParticles : MonoBehaviour
{
    GunData gunData;
    float damageMultiplicator = 1f;

    public void InitGunData(GunData gunData)
    {
        this.gunData = gunData;
    }

    public void UpdateDamageMultiplicator(float damage)
    {
        damageMultiplicator = damage;
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().takeDamage(gunData.damage * damageMultiplicator);
        }
    }

}

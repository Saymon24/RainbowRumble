using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterGunParticles : MonoBehaviour
{
    GunData gunData;

    public void InitGunData(GunData gunData)
    {
        this.gunData = gunData;
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().takeDamage(gunData.damage);
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetGunData : MonoBehaviour
{

    [SerializeField] private GunData gunData;

    public GunData GetData()
    {
        return gunData;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sugar : MonoBehaviour
{
    public void giveEffects()
    {
        GameObject.Find("Player").AddComponent<SpeedUpEffects>();
    }
}

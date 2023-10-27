using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeBiberon : MonoBehaviour
{
    public void giveEffects()
    {
        GameObject.Find("Player").AddComponent<BiberonEffects>();
    }

}

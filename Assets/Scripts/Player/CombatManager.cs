using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public bool isInCombat = false;
    [SerializeField] private Camera _camera;

    public void ActivateCombatMode()
    {
        isInCombat = true;
        AudioManager.instance.PlayMusic("War");
        _camera.fieldOfView = 70f;
        GetComponent<PlayerMovement>().speed = 15f;
    }
}

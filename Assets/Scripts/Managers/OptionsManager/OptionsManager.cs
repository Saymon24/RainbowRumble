using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsManager : MonoBehaviour
{
    GameObject player;

    public void Start()
    {
        player = GameObject.Find("Player");
        UpdateOptions();
    }

    public void UpdateOptions()
    {
        UpdateSensivity();
    }

    private void UpdateSensivity()
    {
        float mouseSensivityX = PlayerPrefs.GetFloat("MouseSensivityX");
        float mouseSensivityY = PlayerPrefs.GetFloat("MouseSensivityY");

        player.GetComponentInChildren<MouseLook>().mouseSensitivityX = mouseSensivityX;
        player.GetComponentInChildren<MouseLook>().mouseSensitivityY = mouseSensivityY;
    }
}

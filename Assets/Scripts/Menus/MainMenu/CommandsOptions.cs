using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommandsOptions : MonoBehaviour
{
    [SerializeField] private Toggle vibrationsToggle;
    [SerializeField] private Toggle aimAssistToggle;

    [SerializeField] private Slider mouseSensivityX;
    [SerializeField] private Slider mouseSensivityY;
    [SerializeField] private Slider controllerSensivityX;
    [SerializeField] private Slider controllerSensivityY;

    private void OnEnable()
    {
        vibrationsToggle.isOn = Convert.ToBoolean(PlayerPrefs.GetInt("HasVibrations", 1));
        aimAssistToggle.isOn = Convert.ToBoolean(PlayerPrefs.GetInt("HasAimAssist", 1));

        mouseSensivityX.value = PlayerPrefs.GetFloat("MouseSensivityX", 100);
        mouseSensivityY.value = PlayerPrefs.GetFloat("MouseSensivityY", 100);
        controllerSensivityX.value = PlayerPrefs.GetFloat("ControllerSensivityX", 100);
        controllerSensivityY.value = PlayerPrefs.GetFloat("ControllerSensivityY", 100);
    }

    public void UpdateVibrations(bool hasVibrations)
    {
        PlayerPrefs.SetInt("HasVibrations", Convert.ToInt32(hasVibrations));
        PlayerPrefs.Save();
    }

    public void UpdateAimAssist(bool hasAimAssist)
    {
        PlayerPrefs.SetInt("HasAimAssist", Convert.ToInt32(hasAimAssist));
        PlayerPrefs.Save();
    }

    public void UpdateMouseSensivityX(float value)
    {
        PlayerPrefs.SetFloat("MouseSensivityX", value);
        PlayerPrefs.Save();
    }

    public void UpdateMouseSensivityY(float value)
    {
        PlayerPrefs.SetFloat("MouseSensivityY", value);
        PlayerPrefs.Save();
    }

    public void UpdateControllerSensivityX(float value)
    {
        PlayerPrefs.SetFloat("ControllerSensivityX", value);
        PlayerPrefs.Save();
    }

    public void UpdateControllerSensivityY(float value)
    {
        PlayerPrefs.SetFloat("ControllerSensivityY", value);
        PlayerPrefs.Save();
    }
}

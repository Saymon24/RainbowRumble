using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class GraphicsOptions : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Dropdown qualityDropdown;
    [SerializeField] private TMPro.TMP_Dropdown windowModeDropdown;
    [SerializeField] private TMPro.TMP_Dropdown resolutionDropdown;
    [SerializeField] private Toggle VsyncToggle;

    private Vector2 resolution;
    private FullScreenMode fullScreenMode;

    private void Awake()
    {
        resolution = new Vector2(Screen.width, Screen.height);
        fullScreenMode = Screen.fullScreenMode;
        Debug.Log(QualitySettings.GetQualityLevel());
        Screen.SetResolution((int)resolution.x, (int)resolution.y, fullScreenMode);
        UpdateGraphicsOptions();
    }

    public void UpdateGraphicsOptions()
    {
        UpdateQuality();
        UpdateWindowMode();
        UpdateWindowResolution();
        UpdateVSYNC();
    }

    private void UpdateQuality()
    {
        int index = QualitySettings.GetQualityLevel();

        qualityDropdown.value = index;
    }

    private void UpdateWindowMode()
    {
        int index = 0;

        if (fullScreenMode == FullScreenMode.ExclusiveFullScreen)
            index = 0;
        if (fullScreenMode == FullScreenMode.FullScreenWindow)
            index = 1;
        if (fullScreenMode == FullScreenMode.Windowed)
            index = 2;

        windowModeDropdown.value = index;
    }

    private void UpdateWindowResolution()
    {
        int index = 0;
        string resolutionTmp = resolution.x.ToString() + "x" + resolution.y.ToString();

        foreach (var resolution in resolutionDropdown.options)
        {
            if (resolution.text == resolutionTmp)
            {
                resolutionDropdown.SetValueWithoutNotify(index);
                return;
            }
            index++;
        }
        resolutionDropdown.value = index;
    }

    private void UpdateVSYNC()
    {
        if (QualitySettings.vSyncCount == 0)
            VsyncToggle.isOn = false;
        else
            VsyncToggle.isOn = true;
    }

    public void ChangeQualitySettings(int index)
    {
        QualitySettings.SetQualityLevel(index, false);
    }

    public void ChangeWindowMode(int index)
    {
        if (index == 0)
            fullScreenMode = FullScreenMode.ExclusiveFullScreen;
        if (index == 1)
            fullScreenMode = FullScreenMode.FullScreenWindow;
        if (index == 2)
            fullScreenMode = FullScreenMode.Windowed;

        Screen.fullScreenMode = fullScreenMode;
    }

    public void ChangeWindowResolution(int index)
    {
        string[] resolutionsTexts = resolutionDropdown.options[index].text.Split("x");

        if (resolutionsTexts.Length != 2)
            resolution = new Vector2(1920, 1080);
        else
            resolution = new Vector2(Int32.Parse(resolutionsTexts[0]), Int32.Parse(resolutionsTexts[1]));
        Screen.SetResolution((int)resolution.x, (int)resolution.y, fullScreenMode);
    }

    public void ActiveVSYNC(bool state)
    {
        if (state)
            QualitySettings.vSyncCount = 1;
        else
            QualitySettings.vSyncCount = 0;
    }
}

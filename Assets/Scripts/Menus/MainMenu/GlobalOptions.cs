using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlobalOptions : MonoBehaviour
{

    [SerializeField] private TMPro.TMP_Dropdown languageTextDropdown;
    [SerializeField] private TMPro.TMP_Dropdown languageAudioDropdown;
    [SerializeField] private Toggle subtitlesToggle;
    [SerializeField] private TMPro.TMP_Dropdown subtitleSizeDropdown;
    [SerializeField] private Slider subtitleOpacitySlider;
    [SerializeField] private Slider brightnessSlider;

    private void OnEnable()
    {
        languageTextDropdown.value = PlayerPrefs.GetInt("LanguageText", 0);
        languageAudioDropdown.value = PlayerPrefs.GetInt("LanguageAudio", 0);

        subtitlesToggle.isOn = Convert.ToBoolean(PlayerPrefs.GetInt("HasSubtitles", 0));
        subtitleSizeDropdown.value = PlayerPrefs.GetInt("SubtitlesSize", 0);
        subtitleOpacitySlider.value = PlayerPrefs.GetFloat("SubtitlesOpacity", 1);

        brightnessSlider.value = PlayerPrefs.GetFloat("Brightness", 1);
    }

    public void UpdateLanguageText(int index)
    {
        PlayerPrefs.SetInt("LanguageText", index);
        PlayerPrefs.Save();
    }

    public void UpdateLanguageAudio(int index)
    {
        PlayerPrefs.SetInt("LanguageAudio", index);
        PlayerPrefs.Save();
    }

    public void UpdateSubtitlesEnable(bool hasSubtitles)
    {
        PlayerPrefs.SetInt("HasSubtitles", Convert.ToInt32(hasSubtitles));
        PlayerPrefs.Save();
    }

    public void UpdateSubtitlesSize(int index)
    {
        PlayerPrefs.SetInt("SubtitlesSize", index);
        PlayerPrefs.Save();
    }

    public void UpdateSubtitlesOpacity(float value)
    {
        PlayerPrefs.SetFloat("SubtitlesOpacity", value);
        PlayerPrefs.Save();
    }

    public void UpdateBrightness(float value)
    {
        PlayerPrefs.SetFloat("Brightness", value);
        PlayerPrefs.Save();
    }
}

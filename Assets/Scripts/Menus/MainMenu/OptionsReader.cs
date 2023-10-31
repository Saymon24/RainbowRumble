using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsReader : MonoBehaviour
{
    void Start()
    {
        LoadAudio();
    }

    private void LoadAudio()
    {
        float masterVolume = PlayerPrefs.GetFloat("MasterVolume", 1f);
        float musicVolume = PlayerPrefs.GetFloat("MusicVolume", 1f);
        float sfxVolume = PlayerPrefs.GetFloat("SfxVolume", 1f);
        float voiceVolume = PlayerPrefs.GetFloat("VoiceVolume", 1f);

        Debug.Log("Player Pref Master: " + PlayerPrefs.GetFloat("MasterVolume"));

        AudioManager.instance.MasterVolume((1 - masterVolume) * -50);
        AudioManager.instance.MusicVolume((1 - musicVolume) * -50);
        AudioManager.instance.SfxVolume((1 - sfxVolume) * -50);
        AudioManager.instance.VoiceVolume((1 - voiceVolume) * -50);
    }
}

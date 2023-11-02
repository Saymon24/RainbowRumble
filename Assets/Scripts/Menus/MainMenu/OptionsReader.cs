using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class OptionsReader : MonoBehaviour
{
    [SerializeField] InputActionAsset inputAsset;

    void Start()
    {
        LoadAudio();
        LoadKeysBinds();
    }

    private void LoadKeysBinds()
    {
        if (!PlayerPrefs.HasKey("rebinds"))
            return;
        string rebindSave = PlayerPrefs.GetString("rebinds");

        inputAsset.LoadBindingOverridesFromJson(rebindSave, true);
    }

    private void LoadAudio()
    {
        float masterVolume = PlayerPrefs.GetFloat("MasterVolume", 1f);
        float musicVolume = PlayerPrefs.GetFloat("MusicVolume", 1f);
        float sfxVolume = PlayerPrefs.GetFloat("SfxVolume", 1f);
        float voiceVolume = PlayerPrefs.GetFloat("VoiceVolume", 1f);

        AudioManager.instance.MasterVolume((1 - masterVolume) * -50);
        AudioManager.instance.MusicVolume((1 - musicVolume) * -50);
        AudioManager.instance.SfxVolume((1 - sfxVolume) * -50);
        AudioManager.instance.VoiceVolume((1 - voiceVolume) * -50);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundPanelController : MonoBehaviour
{
    public Slider _masterSlider, _musicSlider, _sfxSlider, _voiceSlider;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            transform.GetChild(0).gameObject.SetActive(!transform.GetChild(0).gameObject.activeSelf);
            Time.timeScale = transform.GetChild(0).gameObject.activeSelf ? 0 : 1;
            Cursor.lockState = transform.GetChild(0).gameObject.activeSelf ? CursorLockMode.Confined : CursorLockMode.Locked;
        }
    }

    public void ToggleMaster()
    {
        AudioManager.instance.ToggleMaster();
    }

    public void ToggleMusic()
    {
        AudioManager.instance.ToggleMusic();
    }

    public void ToggleSfx()
    {
        AudioManager.instance.ToggleSfx();
    }

    public void ToggleVoice()
    {
        AudioManager.instance.ToggleVoice();
    }

    public void MasterVolume()
    {
        AudioManager.instance.MasterVolume((1 - _masterSlider.value) * -50);
    }

    public void MusicVolume()
    {
        AudioManager.instance.MusicVolume((1 - _musicSlider.value) * -50);
    }

    public void SfxVolume()
    {
        AudioManager.instance.SfxVolume((1 - _sfxSlider.value) * -50);
    }

    public void VoiceVolume()
    {
        AudioManager.instance.VoiceVolume((1 - _voiceSlider.value) * -50);
    }
}

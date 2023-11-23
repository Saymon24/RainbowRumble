using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public Sound[] musicSounds, sfxSounds, voiceSounds;
    public AudioSource musicSource, sfxSource, voiceSource;
    [SerializeField] private AudioMixer _audioMixer;

    private bool _master = true;
    private bool _music = true;
    private bool _sfx = true;
    private bool _voice = true;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else
            Destroy(gameObject);
    }

    public void Start()
    {
    }

    public void PlayMusic(string name)
    {
        Sound s = Array.Find(musicSounds, x => x.name == name);

        if (s == null)
            Debug.LogError("Music not found");
        else if (_music)
        {
            musicSource.clip = s.clip;
            musicSource.Play();
        }
    }

    public void PlaySFX(string name)
    {
        Sound s = Array.Find(sfxSounds, x => x.name == name);

        if (s == null)
            Debug.LogError("Sound not found");
        else if (_sfx)
        {
            sfxSource.PlayOneShot(s.clip);
        }
    }

    public void PlayVoice(string name)
    {
        Sound s = Array.Find(voiceSounds, x => x.name == name);

        if (s == null)
            Debug.LogError("Voice not found");
        else if (_voice)
        {
            voiceSource.PlayOneShot(s.clip);
        }
    }

    public void ToggleMaster()
    {
        _master = !_master;
        if (_master)
        {
            musicSource.mute = false;
            sfxSource.mute = false;
            voiceSource.mute = false;
        } else
        {
            musicSource.mute = true;
            sfxSource.mute = true;
            voiceSource.mute = true;
        }
    }

    public void ToggleMusic()
    {
        musicSource.mute = !musicSource.mute;
        _music = !_music;
    }

    public void ToggleSfx()
    {
        Debug.Log(_sfx);
        sfxSource.mute = !sfxSource.mute;
        _sfx = !_sfx;
        Debug.Log(_sfx);
    }

    public void ToggleVoice()
    {
        voiceSource.mute = !voiceSource.mute;
        _voice = !_voice;
    }

    public void MasterVolume(float volume)
    {
        _audioMixer.SetFloat("Master", volume);
    }

    public float GetMasterVolume()
    {
        float volume;
        if (_audioMixer.GetFloat("Master", out volume))
            return volume;
        else
            return 0;
    }

    public void MusicVolume(float volume)
    {
        _audioMixer.SetFloat("Music", volume);
    }

    public float GetMusicVolume()
    {
        float volume;
        if (_audioMixer.GetFloat("Music", out volume))
            return volume;
        else
            return 0;
    }

    public void SfxVolume(float volume)
    {
        _audioMixer.SetFloat("SFX", volume);
    }

    public float GetSfxVolume()
    {
        float volume;
        if (_audioMixer.GetFloat("SFX", out volume))
            return volume;
        else
            return 0;
    }

    public void VoiceVolume(float volume)
    {
        _audioMixer.SetFloat("Voice", volume);
    }

    public float GetVoiceVolume()
    {
        float volume;
        if (_audioMixer.GetFloat("Voice", out volume))
            return volume;
        else
            return 0;
    }
}

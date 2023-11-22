using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [SerializeField] private List<AudioSource> sfxSources = new List<AudioSource>();
    [SerializeField] private AudioSource bgmSource;
    
    public enum SoundName
    {
        PlayerBites,
        HitPlayer,
        CollectItem,
        PlayerJump,
        PlayerDeath,
        ButtonClicked,
        TakePhoto
    }

    [SerializeField] private Sound[] _sounds;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
    }
    

    public void Play(SoundName name)
    {
        Sound sound = GetSound(name);
        if (sound.audioSource == null)
        {
            sound.audioSource = gameObject.AddComponent<AudioSource>();
            sfxSources.Add(sound.audioSource);
            sound.audioSource.clip = sound.clip;
            sound.audioSource.volume = PlayerPrefs.GetFloat("SFX Volume", 1);
            sound.audioSource.loop = sound.loop;
        }

        sound.audioSource.Play();
    }

    private Sound GetSound(SoundName name)
    {
        return Array.Find(_sounds, s => s.soundName == name);
    }

    public void ToggleBGM()
    {
        bgmSource.mute = !bgmSource.mute;
    }
    
    public void ToggleSFX()
    {
        foreach (AudioSource source in sfxSources)
        {
            source.mute = !source.mute;
        }
        
    }

    public void MusicVolume(float volume)
    {
        PlayerPrefs.SetFloat("BGM Volume", volume);
        bgmSource.volume = PlayerPrefs.GetFloat("BGM Volume", 1);
    }
    
    public void SFXVolume(float volume)
    {
        PlayerPrefs.SetFloat("SFX Volume", volume);
        for (int i = 0; i < sfxSources.Count; i++)
        {
            sfxSources[i].volume = PlayerPrefs.GetFloat("SFX Volume", 1);
        }
            
        
    }
}

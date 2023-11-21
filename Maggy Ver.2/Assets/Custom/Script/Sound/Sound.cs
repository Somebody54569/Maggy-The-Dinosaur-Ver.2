using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    [SerializeField] private SoundManager.SoundName _soundName;
    public SoundManager.SoundName soundName => _soundName;

    [SerializeField] private AudioClip _clip;
    public AudioClip clip => _clip;

    [Range(0f, 1f)]
    [SerializeField] private float _volume = 1f;
    public float volume => _volume;

    [SerializeField] private bool _loop;
    public bool loop => _loop;

    [HideInInspector]
    public AudioSource audioSource;
}

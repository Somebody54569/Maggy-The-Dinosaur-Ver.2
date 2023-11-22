using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISound : MonoBehaviour
{
    [SerializeField] public Slider bgmSlider, sfxSlider;
    // Start is called before the first frame update
    public void ToggleMusic()
    {
        SoundManager.instance.ToggleBGM();
    }

    public void ToggleSFX()
    {
        SoundManager.instance.ToggleSFX();
    }

    public void BGMVolume()
    {
        SoundManager.instance.MusicVolume(bgmSlider.value);
    }

    public void SFXVolume()
    {
        SoundManager.instance.SFXVolume(sfxSlider.value);
    }
}

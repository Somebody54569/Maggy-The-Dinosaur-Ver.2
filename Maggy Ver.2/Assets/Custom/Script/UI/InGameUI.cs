using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameUI : MonoBehaviour
{
    [SerializeField] BannerAdExample bannerAdExample;
    [SerializeField] RewardedAdsButton rewardedAdsButton;
    private void Awake()
    {
        Time.timeScale = 0;
    }

    private void Start()
    {
        rewardedAdsButton.LoadAd();
    }

    public void PauseGame()
        {
            Time.timeScale = 0;
        }
    public void ResumeGame()
        {
            Time.timeScale = 1;
        }

    public void RestartGame()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }

    public void Menu()
    {
        SceneManager.LoadScene(0);
        bannerAdExample.HideBannerAd();
    }

    public void PlaySound()
    {
        SoundManager.instance.Play(SoundManager.SoundName.ButtonClicked);
    }
    
}

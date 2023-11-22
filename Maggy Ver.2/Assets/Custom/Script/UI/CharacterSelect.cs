using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelect : MonoBehaviour
{
    [SerializeField] private Material[] skinMaterials;
    [SerializeField] private int selectedSkin;
    [SerializeField] private SkinnedMeshRenderer skinnedMeshRenderer;
    public Skin[] skin;

    [SerializeField] private Button unlockButton;
    [SerializeField] private TextMeshProUGUI totalBoneText;
    private void Awake()
    {
        skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        selectedSkin = PlayerPrefs.GetInt("SelectedSkin");
        Material[] playerMat = skinnedMeshRenderer.sharedMaterials;
        playerMat[0] = skinMaterials[selectedSkin];
        skinnedMeshRenderer.sharedMaterials = playerMat;

        foreach (Skin s in skin)
        {
            if (s.price == 0)
            {
                s.isUnlocked = true;
            }
            else
            {
                s.isUnlocked = PlayerPrefs.GetInt(s.color, 0) == 0 ? false : true;
            }
        }

        UpdateUI();
    }

    public void ChangeNext()
    {
        selectedSkin++;
        if (selectedSkin == skinMaterials.Length)
        {
            selectedSkin = 0;
        }
        Material[] playerMat = skinnedMeshRenderer.sharedMaterials;
        playerMat[0] = skinMaterials[selectedSkin];
        skinnedMeshRenderer.sharedMaterials = playerMat;

        if (skin[selectedSkin].isUnlocked)
        {
            PlayerPrefs.SetInt("SelectedSkin", selectedSkin);
        }
        Debug.Log(PlayerPrefs.GetInt("SelectedSkin"));
        UpdateUI();
    }
    
    public void ChangePrevious()
    {
        selectedSkin--;
        if (selectedSkin == -1)
        {
            selectedSkin = skinMaterials.Length - 1;
        }
        Material[] playerMat = skinnedMeshRenderer.sharedMaterials;
        playerMat[0] = skinMaterials[selectedSkin];
        skinnedMeshRenderer.sharedMaterials = playerMat;
        
        if (skin[selectedSkin].isUnlocked)
        {
            PlayerPrefs.SetInt("SelectedSkin", selectedSkin);
        }
        Debug.Log(PlayerPrefs.GetInt("SelectedSkin"));
        UpdateUI();
    }

    public void UpdateUI()
    {
        totalBoneText.text = "" + PlayerPrefs.GetInt("totalBones", 0);
        if (skin[selectedSkin].isUnlocked == true)
        {
            unlockButton.gameObject.SetActive(false);
        }
        else
        {
            unlockButton.GetComponentInChildren<TextMeshProUGUI>().text = "Price: " + skin[selectedSkin].price;
            if (PlayerPrefs.GetInt("totalBones", 0) < skin[selectedSkin].price)
            {
                unlockButton.gameObject.SetActive(true);
                unlockButton.interactable = false;
            }
            else
            {
                unlockButton.gameObject.SetActive(true);
                unlockButton.interactable = true;
            }
        }
        
    }

    public void Unlock()
    {
        int bones = PlayerPrefs.GetInt("totalBones", 0);
        int price = skin[selectedSkin].price;
        PlayerPrefs.SetInt("totalBones", bones - price);
        PlayerPrefs.SetInt(skin[selectedSkin].color, 1);
        PlayerPrefs.SetInt("SelectedSkin", selectedSkin);
        skin[selectedSkin].isUnlocked = true;
        UpdateUI();
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelect : MonoBehaviour
{
    [SerializeField] private Material[] skinMaterials;
    [SerializeField] private int selectedSkin;
    [SerializeField] private SkinnedMeshRenderer skinnedMeshRenderer;
    private void Awake()
    {
        skinnedMeshRenderer = FindObjectOfType<SkinnedMeshRenderer>();
        selectedSkin = PlayerPrefs.GetInt("SelectedSkin", 0);
        Material[] playerMat = skinnedMeshRenderer.sharedMaterials;
        playerMat[0] = skinMaterials[selectedSkin];
        skinnedMeshRenderer.sharedMaterials = playerMat;
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
        
        PlayerPrefs.SetInt("SelectedSkin", selectedSkin);
        Debug.Log(PlayerPrefs.GetInt("SelectedSkin"));
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
        
        PlayerPrefs.SetInt("SelectedSkin", selectedSkin);
        Debug.Log(PlayerPrefs.GetInt("SelectedSkin"));
    }
}

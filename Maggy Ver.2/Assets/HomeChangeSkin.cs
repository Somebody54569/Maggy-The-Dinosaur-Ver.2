using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeChangeSkin : MonoBehaviour
{
    [SerializeField] private Material[] skinMaterials;
    [SerializeField] private int selectedSkin;
    [SerializeField] private SkinnedMeshRenderer skinnedMeshRenderer;
    private void Awake()
    {
        skinnedMeshRenderer = FindObjectOfType<SkinnedMeshRenderer>();
    }

    private void Update()
    {
        selectedSkin = PlayerPrefs.GetInt("SelectedSkin", 0);
        Material[] playerMat = skinnedMeshRenderer.sharedMaterials;
        playerMat[0] = skinMaterials[selectedSkin];
        skinnedMeshRenderer.sharedMaterials = playerMat;
    }
}

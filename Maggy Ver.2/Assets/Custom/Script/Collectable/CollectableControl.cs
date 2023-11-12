using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CollectableControl : MonoBehaviour
{
    public static int boneCount;
    public GameObject boneCountDisplay;
    public GameObject boneEndDisplay;

    private void Start()
    {
        boneCount = 0;
    }

    void Update()
    {
        boneCountDisplay.GetComponent<TextMeshProUGUI>().text = $"{boneCount}";
        boneEndDisplay.GetComponent<TextMeshProUGUI>().text = $"{boneCount}";
    }
}

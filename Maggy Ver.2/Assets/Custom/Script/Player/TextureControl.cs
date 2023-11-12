using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureControl : MonoBehaviour
{
    public Material[] skinMaterials; // Drag the new skin material to this field in the Inspector
    public SkinnedMeshRenderer skinnedMeshRenderer;
    void Start()
    {
        skinnedMeshRenderer = GameObject.Find("Retopo").GetComponent<SkinnedMeshRenderer>();
        if (skinnedMeshRenderer != null && skinMaterials != null && skinMaterials.Length > 0)
        {
            // Generate a random index to select a random skin material
            int randomIndex = Random.Range(0, skinMaterials.Length);

            // Get the current materials array
            Material[] materials = skinnedMeshRenderer.sharedMaterials;

            // Assign the randomly selected skin material
            materials[0] = skinMaterials[randomIndex];

            // Assign the modified materials array back to the SkinnedMeshRenderer
            skinnedMeshRenderer.sharedMaterials = materials;
        }
        else
        {
            Debug.LogError("SkinnedMeshRenderer, skinMaterials, or skinMaterials array is invalid. Make sure to assign materials and ensure the array is not empty.");
        }
    }
    
    
}

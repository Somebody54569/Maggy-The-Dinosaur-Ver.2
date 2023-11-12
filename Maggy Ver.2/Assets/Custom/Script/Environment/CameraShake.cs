using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public Transform cameraTransform;
    public float shakeDuration = 0.5f;
    public float shakeMagnitude = 0.2f;
        
    private Vector3 originalPosition;
        
    private void Start()
    {
        if (cameraTransform == null)
        { cameraTransform = Camera.main.transform;
        }
        originalPosition = cameraTransform.localPosition;
    }
    public void Shake()
    {
        StartCoroutine(ShakeCoroutine());
    }
        
    private IEnumerator ShakeCoroutine()
    {
        float elapsed = 0.0f;
        
        while (elapsed < shakeDuration)
        {
            float x = originalPosition.x + Random.Range(-1f, 1f) * shakeMagnitude;
            float y = originalPosition.y + Random.Range(-1f, 1f) * shakeMagnitude;
        
            cameraTransform.localPosition = new Vector3(x, y, originalPosition.z);
        
            elapsed += Time.deltaTime;
        
            yield return null;
        }
        cameraTransform.localPosition = originalPosition;
    }
}

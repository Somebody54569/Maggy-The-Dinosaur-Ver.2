using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class TakeDamageFlash : MonoBehaviour
{
    [SerializeField] private float intensity = 0;

    private PostProcessVolume _volume;
    private Vignette _vignette;
    void Start()
    {
        _volume = GetComponent<PostProcessVolume>();

        _volume.profile.TryGetSettings<Vignette>(out _vignette);

        if (!_vignette)
        {
            Debug.Log("Error, vignette Empty");
        }
        else
        {
            _vignette.enabled.Override(false);
        }
    }

    public IEnumerator TakeDamageEffect()
    {
        intensity = 0.4f;
        _vignette.enabled.Override(true);
        _vignette.intensity.Override(0.4f);
        yield return new WaitForSeconds(0.4f);

        while (intensity > 0)
        {
            intensity -= 0.01f;
            if (intensity < 0)
            {
                intensity = 0;
            }
                    
            _vignette.intensity.Override(intensity);
            
            yield return new WaitForSeconds(0.1f);
        }
        
        _vignette.enabled.Override(false);
        yield break;
    }
}

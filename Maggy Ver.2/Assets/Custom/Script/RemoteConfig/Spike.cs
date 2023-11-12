using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.RemoteConfig;

public class Spike : MonoBehaviour
{
    public struct userAttributes { }
    public struct appAttributes { }

    public bool haveSpike;

    public GameObject spike;
    // Start is called before the first frame update
    IEnumerator Start()
    {
        while (!RemoteConfigSetting.Instance)
        {
            yield return null;
        }

        RemoteConfigService.Instance.FetchCompleted += ApplySpikeRemote;
        RemoteConfigService.Instance.FetchConfigs<userAttributes, appAttributes>(new userAttributes(), 
            new appAttributes());
    }

    private void OnDestroy()
    {
        RemoteConfigService.Instance.FetchCompleted -= ApplySpikeRemote;
    }

    void ApplySpikeRemote(ConfigResponse response)
    {
        haveSpike = RemoteConfigService.Instance.appConfig.GetBool("haveSpike");
        if (haveSpike)
        {
            SetUpSpike();
        }
        else
        {
            SetUpDefault();
        }
    }

    void SetUpSpike()
    {
        spike.SetActive(true);
    }

    void SetUpDefault()
    {
        spike.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

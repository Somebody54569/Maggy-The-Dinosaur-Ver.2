using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Analytics;
using Unity.Services.Core;
using Unity.Services.PushNotifications;
using System.Threading.Tasks;
using System;
using TMPro;
public class Notification : MonoBehaviour
{
    public TextMeshProUGUI deviceTokenText;
    public TextMeshProUGUI debugText;

    private bool userGaveConsent = false;
    // Start is called before the first frame update
    public async Task Start()
    {
        if (userGaveConsent)
        {
            AnalyticsService.Instance.StartDataCollection();
        }

        try
        {
            string pushToken = await PushNotificationsService.Instance.RegisterForPushNotificationsAsync();
            PushNotificationsService.Instance.OnNotificationReceived += notificationData =>
            {
                deviceTokenText.text = pushToken;
                Debug.Log("Received a notification!");
                debugText.text = "Received a notification!";
            };
        }
        catch (Exception e)
        {
            Debug.Log("Failed to retrieve a push notification token.");
            debugText.text = "Failed to retrieve a push notification token.";
        }
    }

    public async Task InitialNotificationAsync()
    {
        await UnityServices.InitializeAsync();
    }

    async void Awake()
    {
        await InitialNotificationAsync();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

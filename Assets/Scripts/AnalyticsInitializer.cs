using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Analytics;

public class AnalyticsInitializer : MonoBehaviour
{
    async void Awake()
    {
        try
        {
            await UnityServices.InitializeAsync();
            Debug.Log("UGS Initialized");
        }
        catch (System.Exception e)
        {
            Debug.LogError("UGS Initialization failed: " + e.Message);
        }
    }
}

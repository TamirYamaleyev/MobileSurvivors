using UnityEngine;
using Unity.Services.Analytics;
using Unity.Services.Core;
using System.Threading.Tasks;

public class AnalyticsManager : MonoBehaviour
{
    public static AnalyticsManager Instance { get; private set; }

    async void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        await UnityServices.InitializeAsync();

        Debug.Log("Unity Services Initialized — analytics ready");
    }

    public void TrackSessionStart()
    {
        AnalyticsService.Instance.RecordEvent("session_start");
        Debug.Log("Analytics: session_start recorded");
    }

    public void TrackDailyBonus()
    {
        AnalyticsService.Instance.RecordEvent("daily_bonus_collected");
        Debug.Log("Analytics: daily_bonus_collected recorded");
    }
}

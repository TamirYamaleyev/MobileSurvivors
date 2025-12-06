using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneButton : MonoBehaviour
{
    public DailyBonusManager dailyBonus;
    public GameObject dailyBonusImage;

    void Start()
    {
        if (dailyBonus == null)
            dailyBonus = FindAnyObjectByType<DailyBonusManager>();

        bool available = dailyBonus.IsEligibleForBonus();

        dailyBonusImage.SetActive(available);
    }

    public void LoadScene(string sceneName)
    {
        if (dailyBonus != null && dailyBonus.IsEligibleForBonus())
        {
            ClaimDailyBonus();
        }
        
        SceneManager.LoadScene(sceneName);
    }

    public void ClaimDailyBonus()
    {
        dailyBonus.UpdateLastPlayTime();

        FindAnyObjectByType<DailyNotificationManager>().ScheduleDailyBonusNotification();

        AnalyticsManager.Instance.TrackDailyBonus();

        Debug.Log("Daily Bonus Claimed!");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

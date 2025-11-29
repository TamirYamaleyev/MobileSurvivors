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
            dailyBonus.UpdateLastPlayTime();
            FindAnyObjectByType<DailyNotificationManager>().ScheduleDailyBonusNotification();
            Debug.Log("Daily Bonus Claimed!");
        }
        
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

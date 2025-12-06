using UnityEngine;
using System;

public class DailyBonusManager : MonoBehaviour
{
    private const string LastPlayKey = "LastPlayTime";

    public bool IsEligibleForBonus()
    {
        if (!PlayerPrefs.HasKey(LastPlayKey))
            return true; // First time playing: give bonus

        string savedTime = PlayerPrefs.GetString(LastPlayKey);
        DateTime lastPlay = DateTime.Parse(savedTime);
        TimeSpan timePassed = DateTime.Now - lastPlay;

        return timePassed.TotalHours >= 24;
    }

    public void UpdateLastPlayTime()
    {
        PlayerPrefs.SetString(LastPlayKey, DateTime.Now.ToString());
        PlayerPrefs.Save();
    }
}

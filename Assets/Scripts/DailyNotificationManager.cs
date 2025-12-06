using UnityEngine;
using Unity.Notifications.Android;
using System;

public class DailyNotificationManager : MonoBehaviour
{
    private const string notificationIdKey = "DailyNotificationId";

    void Start()
    {
        CreateAndroidChannel();
    }

    private void CreateAndroidChannel()
    {
        // Only need to create once
        var channel = new AndroidNotificationChannel()
        {
            Id = "daily_bonus_channel",
            Name = "Daily Bonus",
            Importance = Importance.Default,
            Description = "Reminder to collect your daily bonus",
        };
        AndroidNotificationCenter.RegisterNotificationChannel(channel);
    }

    public void ScheduleDailyBonusNotification()
    {
        // Cancel previous notification if exists
        if (PlayerPrefs.HasKey(notificationIdKey))
        {
            int id = PlayerPrefs.GetInt(notificationIdKey);
            AndroidNotificationCenter.CancelNotification(id);
        }

        var notification = new AndroidNotification
        {
            Title = "Daily Bonus!",
            Text = "Come back and claim your daily bonus!",
            SmallIcon = "icon_0", // default Unity icon
            FireTime = DateTime.Now.AddHours(24)
        };

        int notificationId = AndroidNotificationCenter.SendNotification(notification, "daily_bonus_channel");
        PlayerPrefs.SetInt(notificationIdKey, notificationId);
        PlayerPrefs.Save();
    }
}

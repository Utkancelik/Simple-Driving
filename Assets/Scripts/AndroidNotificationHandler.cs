using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_ANDROID
using Unity.Notifications.Android;
#endif
using System;

public class AndroidNotificationHandler : MonoBehaviour
{
#if UNITY_ANDROID
    private const string ChannelId = "notification_channel";
    public void ScheduleNotification(DateTime dateTime)
    {
        AndroidNotificationChannel notificationChannel = new AndroidNotificationChannel
        {
            Id = ChannelId,
            Name = "Notification Channel",
            Description = "Some random description",
            Importance = Importance.Default
        };

        AndroidNotificationCenter.RegisterNotificationChannel(notificationChannel);

        AndroidNotification androidNotification = new AndroidNotification
        {
            Title = "Energy Recharged!",
            Text = "Your energy recharged, come back DRIVE!",
            SmallIcon = "icon_0",
            LargeIcon = "icon_1",
            FireTime = dateTime
        };

        AndroidNotificationCenter.SendNotification(androidNotification, ChannelId);
    }
#endif
}

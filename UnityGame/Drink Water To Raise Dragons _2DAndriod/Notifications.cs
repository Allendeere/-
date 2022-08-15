using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Notifications.Android;

public class Notifications : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var c = new AndroidNotificationChannel()
        {
            Id = "channel_id",
            Name = "Default Channel",
            Importance = Importance.High,
            Description = "Generic notifications",
        };
        AndroidNotificationCenter.RegisterNotificationChannel(c);
    }

    public static void SendMessage()
    {
        var notification = new AndroidNotification();
        notification.Title = "³Ü¤ô";
        notification.LargeIcon = "largel_icon";
        notification.FireTime = System.DateTime.Now.AddSeconds(6600);

        AndroidNotificationCenter.SendNotification(notification, "channel_id");
    }
}

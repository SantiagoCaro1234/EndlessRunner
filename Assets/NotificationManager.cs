//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using System;
//using System.Runtime.CompilerServices;

//#if UNITY_ANDROID
//using Unity.Notifications.Android;
//#endif

//public class NotificationManager : MonoBehaviour
//{
//    public void ActivateNotification()
//    {
//        DateTime activateDate = DateTime.Now.AddSeconds(10f);
//#if UNITY_ANDROID
//        CreateNotification(activateDate);
//#endif
//    }

//#if UNITY_ANDROID
//    private const string channelID = "notifChannel";

//    public void CreateNotification(DateTime date)
//    {
//        AndroidNotificationChannel androidNotificationChannel = new AndroidNotificationChannel
//        {
//            Id = channelID,
//            Name = "NotificationChannel",
//            Description = "Notification Channel",
//            Importance = Importance.Default
//        };
//        AndroidNotificationCenter.RegisterNotificationChannel(androidNotificationChannel);

//        AndroidNotification androidNotification = new AndroidNotification()
//        {
//            Title = "Come keep Playing",
//            Text = "Pingurush is waiting for you!",
//            SmallIcon = "default",
//            LargeIcon = "default",
//            FireTime = date
//        };

//        AndroidNotificationCenter.SendNotification(androidNotification, channelID);

//    } 
//#endif


//}

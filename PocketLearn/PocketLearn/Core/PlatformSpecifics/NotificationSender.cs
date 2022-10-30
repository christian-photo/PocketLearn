using Android.App;
using Android.Content;
using Android.Media;
using Android;
using PocketLearn.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace PocketLearn.Core.PlatformSpecifics
{
    public class IOSNotificationSender : INotificationSender
    {
        public void SendNotification(string message, NotificationArguments parameter)
        {
            throw new NotImplementedException();
        }

        public void SendNotification(string message, NotificationArguments parameter, Guid projectID)
        {
            throw new NotImplementedException();
        }
    }
}

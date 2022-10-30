using System;
using PocketLearn.Shared.Core.Interfaces;
using PocketLearn.Shared.Core;

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

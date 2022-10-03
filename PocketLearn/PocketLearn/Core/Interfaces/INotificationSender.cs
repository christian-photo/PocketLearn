using System;
using System.Collections.Generic;
using System.Text;

namespace PocketLearn.Core.Interfaces
{
    public interface INotificationSender
    {
        void SendNotification(string message, NotificationArguments parameter);
    }
}

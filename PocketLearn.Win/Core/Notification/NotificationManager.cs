using Microsoft.Toolkit.Uwp.Notifications;
using PocketLearn.Core;
using System;

namespace PocketLearn.Win.Core.Notification
{
    public class NotificationManager
    {
        public static void Handle(ToastNotificationActivatedEventArgsCompat e)
        {
            if (e.Argument == NotificationArguments.LEARN.Argument)
            {
                // DO Stuff
            }
            throw new NotImplementedException();
        }
    }
}

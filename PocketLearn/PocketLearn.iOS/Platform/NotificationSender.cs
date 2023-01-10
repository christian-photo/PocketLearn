#region "copyright"

/*
    Copyright © 2023 Christian Palm (christian@palm-family.de)
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at http://mozilla.org/MPL/2.0/.
*/

#endregion "copyright"

using Foundation;
using PocketLearn.Shared.Core;
using PocketLearn.Shared.Core.Interfaces;
using System;
using UIKit;

namespace PocketLearn.iOS.Platform
{
    public class NotificationSender : INotificationSender
    {
        public void SendNotification(string message, NotificationArguments parameter)
        {
            UILocalNotification notification = new UILocalNotification();
            notification.FireDate = NSDate.FromTimeIntervalSinceNow(5);
            notification.AlertAction = parameter.ToString();
            notification.AlertBody = message;
            UIApplication.SharedApplication.ScheduleLocalNotification(notification);
            UIApplication.SharedApplication.ApplicationIconBadgeNumber++;
        }

        public void SendNotification(string message, NotificationArguments parameter, Guid projectID)
        {
            UILocalNotification notification = new UILocalNotification();
            notification.FireDate = NSDate.FromTimeIntervalSinceNow(5);
            notification.AlertAction = parameter.ToString();
            notification.AlertBody = message;
            UIApplication.SharedApplication.ScheduleLocalNotification(notification);
            UIApplication.SharedApplication.ApplicationIconBadgeNumber++;
        }
    }
}
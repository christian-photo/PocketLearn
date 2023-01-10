#region "copyright"

/*
    Copyright © 2023 Christian Palm (christian@palm-family.de)
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at http://mozilla.org/MPL/2.0/.
*/

#endregion "copyright"

using Android.App;
using System;
using Android.Support.V4.App;
using PocketLearn.Shared.Core.Interfaces;
using PocketLearn.Shared.Core;

namespace PocketLearn.Droid.Platform
{
    public class AndroidNotificationSender : INotificationSender
    {
        public void SendNotification(string message, NotificationArguments parameter)
        {
            // Instantiate the builder and set notification elements:
            NotificationCompat.Builder builder = new NotificationCompat.Builder(MainActivity.Instance, MainActivity.Instance.ChannelID)
                .SetContentTitle("PocketLearn")
                .SetContentText(message)
                .SetSubText(parameter.Argument)
                .SetSmallIcon(Resource.Drawable.ApplicationIcon_24px_A);

            // Build the notification:
            Notification notification = builder.Build();

            NotificationManager.FromContext(MainActivity.Instance).Notify(new Random().Next(), notification);
        }

        public void SendNotification(string message, NotificationArguments parameter, Guid projectID)
        {
            // Instantiate the builder and set notification elements:
            NotificationCompat.Builder builder = new NotificationCompat.Builder(MainActivity.Instance, MainActivity.Instance.ChannelID)
                .SetContentTitle("PocketLearn")
                .SetSubText(parameter.Argument + "&" + projectID.ToString())
                .SetContentText(message)
                .SetSmallIcon(Resource.Drawable.ApplicationIcon_24px_A);

            // Build the notification:
            Notification notification = builder.Build();

            NotificationManager.FromContext(MainActivity.Instance).Notify(new Random().Next(), notification);
        }
    }
}
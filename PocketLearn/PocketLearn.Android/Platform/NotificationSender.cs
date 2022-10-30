using PocketLearn.Core.Interfaces;
using PocketLearn.Core;
using Android.App;
using Android.Media;
using System;
using PocketLearn.Droid;
using Android.Content;
using Android.Support.V4.App;

namespace PlatformMediator.PocketLearn.Droid.Platform
{
    public class AndroidNotificationSender : INotificationSender
    {
        public void SendNotification(string message, NotificationArguments parameter)
        {
            // Instantiate the builder and set notification elements:
            NotificationCompat.Builder builder = new NotificationCompat.Builder(MainActivity.Instance, MainActivity.Instance.ChannelID)
                .SetContentTitle("Sample Notification")
                .SetContentText("Hello World! This is my first notification!")
                .SetSubText(parameter.Argument)
                .SetSmallIcon(Resource.Drawable.icon_about);

            // Build the notification:
            Notification notification = builder.Build();

            // Get the notification manager:
            NotificationManager notificationManager =
                MainActivity.Instance.GetSystemService(Context.NotificationService) as NotificationManager;

            // Publish the notification:
            const int notificationId = 0;
            notificationManager.Notify(notificationId, notification);
        }

        public void SendNotification(string message, NotificationArguments parameter, Guid projectID)
        {
            // Instantiate the builder and set notification elements:
            NotificationCompat.Builder builder = new NotificationCompat.Builder(MainActivity.Instance, MainActivity.Instance.ChannelID)
                .SetContentTitle("Sample Notification")
                .SetContentText("Hello World! This is my first notification!")
                .SetSubText(parameter.Argument + "&" + projectID.ToString())
                .SetSmallIcon(Resource.Drawable.ApplicationIcon);

            // Build the notification:
            Notification notification = builder.Build();

            // Get the notification manager:
            NotificationManager notificationManager =
                MainActivity.Instance.GetSystemService(Context.NotificationService) as NotificationManager;

            // Publish the notification:
            const int notificationId = 0;
            notificationManager.Notify(notificationId, notification);
        }
    }
}
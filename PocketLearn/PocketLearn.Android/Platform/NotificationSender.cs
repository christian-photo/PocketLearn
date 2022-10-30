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
                .SetSubText(parameter.Argument)
                .SetSmallIcon(Resource.Drawable.ApplicationIcon_24px_A);

            // Build the notification:
            Notification notification = builder.Build();

            NotificationManager.FromContext(MainActivity.Instance).Notify(new Random().Next(), notification);
        }
    }
}
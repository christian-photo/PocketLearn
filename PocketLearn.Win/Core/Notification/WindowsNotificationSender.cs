using Microsoft.Toolkit.Uwp.Notifications;
using System;

namespace PocketLearn.Core.Interfaces.Classes
{
    public class WindowsNotificationSender : INotificationSender
    {
        public void SendNotification(string message, NotificationArguments parameter)
        {
            new ToastContentBuilder()
                .AddText("PocketLearn")
                .AddText(message)
                .AddArgument(parameter.Argument)
                .Show();
        }

        public void SendNotification(string message, NotificationArguments parameter, Guid projectID)
        {
            new ToastContentBuilder()
                .AddText("PocketLearn")
                .AddText(message)
                .AddArgument(parameter.Argument + "&" + projectID.ToString())
                .Show();
        }
    }
}

using Microsoft.Toolkit.Uwp.Notifications;

namespace PocketLearn.Core.Interfaces.Classes
{
    public class WindowsNotificationSender : INotificationSender
    {
        public void SendNotification(string message, NotificationArguments parameter)
        {
            new ToastContentBuilder()
                .SetBackgroundActivation()
                .AddText("PocketLearn")
                .AddText(message)
                .AddButton("Learn now", ToastActivationType.Foreground, parameter.Argument)
                .Show();
        }
    }
}

using Microsoft.Toolkit.Uwp.Notifications;
using PocketLearn.Shared.Core;
using PocketLearn.Shared.Core.Learning;
using System;

namespace PocketLearn.Win.Core.Notification
{
    public class NotificationManager
    {
        public static void Handle(ToastNotificationActivatedEventArgsCompat e)
        {
            string[] split = e.Argument.Split('&');
            if (split[0] == NotificationArguments.LEARN.Argument)
            {
                Guid projectGuid = Guid.Parse(split[1]);
                LearnProject project = ProjectManager.GetProjectByID(projectGuid);
                // Open learn window
            }
            throw new NotImplementedException();
        }
    }
}

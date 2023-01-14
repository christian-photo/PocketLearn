#region "copyright"

/*
    Copyright © 2023 Christian Palm (christian@palm-family.de)
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at http://mozilla.org/MPL/2.0/.
*/

#endregion "copyright"

using Microsoft.Toolkit.Uwp.Notifications;
using PocketLearn.Shared.Core;
using PocketLearn.Shared.Core.Learning;
using PocketLearn.Win.MVVM.ViewModel;
using Serilog;
using System;
using System.Threading.Tasks;

namespace PocketLearn.Win.Core.Notification
{
    public class NotificationManager
    {
        [STAThread]
        public static void Handle(ToastNotificationActivatedEventArgsCompat e)
        {
            Log.Debug("Clicked on notification: {ex}", e.Argument);
            Task t = new(() =>
            {
                string[] split = e.Argument.Split('&');
                if (split[0] == NotificationArguments.LEARN.Argument)
                {
                    Guid projectGuid = Guid.Parse(split[1]);
                    LearnProject project = ProjectManager.GetProjectByID(projectGuid);
                    MainWindowVM.Instance.QuestionVM = new QuestionVM(project);
                    MainWindowVM.Instance.AnswerVM = new AnswerVM(project);

                    Utility.NavigateToPage(ApplicationConstants.QuestionViewURI);
                    return;
                }
            });
            BackgroundTask.SentNotification = false;
            t.Start(MainWindowVM.UIContext);
        }
    }
}

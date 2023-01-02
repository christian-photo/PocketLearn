using Microsoft.Toolkit.Uwp.Notifications;
using PocketLearn.Shared.Core;
using PocketLearn.Shared.Core.Learning;
using PocketLearn.Win.MVVM.ViewModel;
using System;
using System.Threading.Tasks;

namespace PocketLearn.Win.Core.Notification
{
    public class NotificationManager
    {
        [STAThread]
        public static void Handle(ToastNotificationActivatedEventArgsCompat e)
        {
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

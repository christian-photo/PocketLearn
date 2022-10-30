using PocketLearn.Shared.Core.Interfaces;
using PocketLearn.Shared.Core.Learning;
using System.Collections.Generic;
using System.Timers;

namespace PocketLearn.Shared.Core
{
    public class BackgroundTask
    {
        private Timer _timer;
        private INotificationSender notification;
        private ProjectManager manager;
        public BackgroundTask(INotificationSender sender, ProjectManager projectmanager)
        {
            notification = sender;
            manager = projectmanager;
        }

        public void Start(int interval = 5)
        {
            _timer = new Timer(interval * 60 * 1000); // every 5 minutes
            _timer.Elapsed += RequestLearnIfNeeded;
            _timer.Start();
        }

        private void RequestLearnIfNeeded(object sender, ElapsedEventArgs e)
        {
            _timer.Stop();
            List<LearnProject> projects = new List<LearnProject>();
            foreach (LearnProject project in manager.LearnProjects)
            {
                if (project.ShouldLearn())
                {
                    projects.Add(project);
                }
            }
            foreach (LearnProject proj in projects)
                notification.SendNotification($"Learn now {proj.ProjectName}!", NotificationArguments.LEARN, proj.ProjectID);
            _timer.Start();
        }

        public void Stop()
        {
            _timer.Close();
        }
    }
}

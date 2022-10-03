using PocketLearn.Core.Interfaces;
using PocketLearn.Core.Learning;
using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;

namespace PocketLearn.Core
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

        public void Start()
        {
            _timer = new Timer(5 * 60 * 1000); // every 5 minutes
            _timer.Elapsed += RequestLearnIfNeeded;
            _timer.Start();
        }

        private void RequestLearnIfNeeded(object sender, ElapsedEventArgs e)
        {
            _timer.Stop();
            foreach (LearnProject project in manager.LearnProjects)
            {
                if (project.ShouldLearn())
                {
                    notification.SendNotification("Learn now!", NotificationArguments.LEARN);
                }
            }
            _timer.Start();
        }
    }
}

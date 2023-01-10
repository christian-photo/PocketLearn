#region "copyright"

/*
    Copyright © 2023 Christian Palm (christian@palm-family.de)
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at http://mozilla.org/MPL/2.0/.
*/

#endregion "copyright"

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

        public delegate void EventArgs(object sender, EventArgs e);

        public static bool SentNotification { get; set; }

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
            if (SentNotification)
            {
                _timer.Start();
                return;
            }
            SentNotification = true;
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

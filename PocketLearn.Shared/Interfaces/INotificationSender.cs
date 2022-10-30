using System;

namespace PocketLearn.Shared.Core.Interfaces
{
    public interface INotificationSender
    {
        /// <summary>
        /// Sends a notification to the device
        /// </summary>
        /// <param name="message">The message to be sent</param>
        /// <param name="parameter">The action to be expected upon click</param>
        void SendNotification(string message, NotificationArguments parameter);

        /// <summary>
        /// Sends Notification containing the action and projectID (to find the corresponding project), seperated by '&'
        /// </summary>
        /// <param name="message">The message to be sent</param>
        /// <param name="parameter">What action to be expected upon click</param>
        /// <param name="projectID">The projectID of the needed project</param>
        void SendNotification(string message, NotificationArguments parameter, Guid projectID);
    }
}

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
using PocketLearn.Shared.Core.Interfaces;
using Serilog;
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

            Log.Debug("Sended notification with argument: {param}", parameter.Argument);
        }

        public void SendNotification(string message, NotificationArguments parameter, Guid projectID)
        {
            new ToastContentBuilder()
                .AddText("PocketLearn")
                .AddText(message)
                .AddArgument(parameter.Argument + "&" + projectID.ToString())
                .Show();

            Log.Debug("Sended notification with argument: {param}", parameter.Argument);
        }
    }
}

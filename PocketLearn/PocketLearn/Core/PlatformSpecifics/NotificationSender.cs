#region "copyright"

/*
    Copyright © 2023 Christian Palm (christian@palm-family.de)
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at http://mozilla.org/MPL/2.0/.
*/

#endregion "copyright"

using System;
using PocketLearn.Shared.Core.Interfaces;
using PocketLearn.Shared.Core;

namespace PocketLearn.Core.PlatformSpecifics
{
    public class IOSNotificationSender : INotificationSender
    {
        public void SendNotification(string message, NotificationArguments parameter)
        {
            throw new NotImplementedException();
        }

        public void SendNotification(string message, NotificationArguments parameter, Guid projectID)
        {
            throw new NotImplementedException();
        }
    }
}

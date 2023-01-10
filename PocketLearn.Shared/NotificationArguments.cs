#region "copyright"

/*
    Copyright © 2023 Christian Palm (christian@palm-family.de)
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at http://mozilla.org/MPL/2.0/.
*/

#endregion "copyright"

using System;
using System.Collections.Generic;
using System.Text;

namespace PocketLearn.Shared.Core
{
    public class NotificationArguments
    {
        public readonly string Argument;
        private NotificationArguments(string arg)
        {
            Argument = arg;
        }

        public static NotificationArguments LEARN = new NotificationArguments("learn");
    }
}

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

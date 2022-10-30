using PocketLearn.Core.Interfaces;
using PocketLearn.Core.PlatformSpecifics.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PocketLearn.Core.PlatformSpecifics
{
    public class PlatformMediator
    {
        public DevicePlatform DevicePlatform { get; private set; }
        public IApplicationConstants ApplicationConstants { get; private set; }
        public INotificationSender NotificationSender { get; private set; }

        public PlatformMediator(DevicePlatform platform)
        {
            DevicePlatform = platform;
        }

        public void RegisterServices(IApplicationConstants constants,
                                     INotificationSender notification)
        {
            ApplicationConstants = constants;
            NotificationSender = notification;
        }
    }
}

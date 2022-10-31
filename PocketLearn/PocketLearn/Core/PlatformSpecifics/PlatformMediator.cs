using PocketLearn.Core.PlatformSpecifics.Interfaces;
using PocketLearn.Shared.Core.Interfaces;

namespace PocketLearn.Core.PlatformSpecifics
{
    public class PlatformMediator
    {
        public DevicePlatform DevicePlatform { get; private set; }
        public IApplicationConstants ApplicationConstants { get; private set; }
        public INotificationSender NotificationSender { get; private set; }
        public IGetFileStream GetFileStream { get; private set; }

        public PlatformMediator(DevicePlatform platform)
        {
            DevicePlatform = platform;
        }

        public void RegisterServices(IApplicationConstants constants,
                                     INotificationSender notification,
                                     IGetFileStream getFileStream)
        {
            ApplicationConstants = constants;
            NotificationSender = notification;
            GetFileStream = getFileStream;
        }
    }
}

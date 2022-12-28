using PocketLearn.Core.PlatformSpecifics.Interfaces;
using PocketLearn.Shared.Core.Interfaces;

namespace PocketLearn.Core.PlatformSpecifics
{
    public class PlatformMediator
    {
        public DevicePlatform DevicePlatform { get; private set; }
        public IApplicationConstants ApplicationConstants { get; private set; }
        public INotificationSender NotificationSender { get; private set; }
        public IQrScanner QrScanner { get; private set; }
        public IFile FileOperations { get; private set; }

        public PlatformMediator(DevicePlatform platform)
        {
            DevicePlatform = platform;
        }

        public void RegisterServices(IApplicationConstants constants,
                                     INotificationSender notification,
                                     IQrScanner scanner,
                                     IFile file)
        {
            ApplicationConstants = constants;
            NotificationSender = notification;
            QrScanner = scanner;
            FileOperations = file;
        }
    }
}

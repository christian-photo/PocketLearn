using Foundation;
using PocketLearn.Core.PlatformSpecifics;
using PocketLearn.iOS.Platform;
using PocketLearn.ViewModels;
using UIKit;

namespace PocketLearn.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();
            var notificationSettings = UIUserNotificationSettings.GetSettingsForTypes(
                    UIUserNotificationType.Alert | UIUserNotificationType.Badge | UIUserNotificationType.Sound, null);

            app.RegisterUserNotificationSettings(notificationSettings);
            PlatformMediator plat = new PlatformMediator(DevicePlatform.iOS);
            plat.RegisterServices(new ApplicationConstants(), new NotificationSender(), new QrScanner(), new FileOperations());
            LoadApplication(new App(plat));
            UIApplication.SharedApplication.ApplicationIconBadgeNumber = 0;
            HomeViewModel.Instance.ProjectListViewModel.BackgroundTask.OnNotificationSent += BackgroundTask_OnNotificationSent; ;

            return base.FinishedLaunching(app, options);
        }

        private void BackgroundTask_OnNotificationSent(object sender, Shared.Core.BackgroundTask.EventArgs e)
        {
            UIApplication.SharedApplication.ApplicationIconBadgeNumber++;
        }
    }
}

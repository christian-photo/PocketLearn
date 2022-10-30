using System;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android;
using AndroidX.Core.App;
using PocketLearn.Core.PlatformSpecifics;
using PlatformMediator.PocketLearn.Droid.Platform;
using PocketLearn.Shared.Core;

namespace PocketLearn.Droid
{
    [Activity(Label = "PocketLearn", Icon = "@drawable/ApplicationIcon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        public static MainActivity Instance { get; private set; }
        public string ChannelID { get; private set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            Core.PlatformSpecifics.PlatformMediator plat = new(DevicePlatform.Android);
            plat.RegisterServices(new AndroidConstants(), new AndroidNotificationSender());

            LoadApplication(new App(plat));
            Instance = this;

            CreateNotificationChannel();

            if (CheckSelfPermission(Manifest.Permission.Camera) != (int)Permission.Granted)
            {
                GetCameraPermissionAsync();
            }

            App.PlatformMediator.NotificationSender.SendNotification("asf", NotificationArguments.LEARN);
        }

        void GetCameraPermissionAsync()
        {
            const string permission = Manifest.Permission.Camera;
            int requestCode = 0;

            if (CheckSelfPermission(permission) == (int)Permission.Granted)
            {
                return;
            }

            ActivityCompat.RequestPermissions(this, new String[] { permission }, requestCode);
            return;
        }

        void CreateNotificationChannel()
        {
            if (Build.VERSION.SdkInt < BuildVersionCodes.O)
            {
                // Notification channels are new in API 26 (and not a part of the
                // support library). There is no need to create a notification
                // channel on older versions of Android.
                return;
            }

            var channelName = "PocketLearn";
            var channelDescription = "PocketLearn Notification";
            ChannelID = Guid.NewGuid().ToString();
            var channel = new NotificationChannel(ChannelID, channelName, NotificationImportance.Default)
            {
                Description = channelDescription
            };

            var notificationManager = (NotificationManager)GetSystemService(NotificationService);
            notificationManager.CreateNotificationChannel(channel);
        }
    }
}
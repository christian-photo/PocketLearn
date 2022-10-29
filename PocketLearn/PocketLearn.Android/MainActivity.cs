using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using Android;
using Android.Widget;
using System.Threading.Tasks;
using static AndroidX.Activity.Result.Contract.ActivityResultContracts;
using AndroidX.Core.Content;
using AndroidX.Core.App;

namespace PocketLearn.Droid
{
    [Activity(Label = "PocketLearn", Icon = "@drawable/ApplicationIcon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());

            if (CheckSelfPermission(Manifest.Permission.Camera) != (int)Permission.Granted)
            {
                GetCameraPermissionAsync();
            }
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
    }
}
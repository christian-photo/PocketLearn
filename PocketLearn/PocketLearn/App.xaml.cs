using PocketLearn.Core.PlatformSpecifics;
using Xamarin.Forms;

namespace PocketLearn
{
    public partial class App : Application
    {
        public static PlatformMediator PlatformMediator { get; private set; }

        public App(PlatformMediator platform)
        {
            InitializeComponent();
            PlatformMediator = platform;
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}

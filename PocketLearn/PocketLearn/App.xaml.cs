using PocketLearn.Core.PlatformSpecifics;
using PocketLearn.Services;
using PocketLearn.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PocketLearn
{
    public partial class App : Application
    {
        public static PlatformMediator PlatformMediator { get; private set; }

        public App(PlatformMediator platform)
        {
            InitializeComponent();
            PlatformMediator = platform;
            DependencyService.Register<MockDataStore>();
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

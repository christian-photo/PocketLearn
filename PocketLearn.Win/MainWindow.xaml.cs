#region "copyright"

/*
    Copyright © 2023 Christian Palm (christian@palm-family.de)
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at http://mozilla.org/MPL/2.0/.
*/

#endregion "copyright"

using PocketLearn.Public.Core.Config;
using PocketLearn.Win.Core;
using PocketLearn.Win.MVVM.ViewModel;
using Serilog;
using System;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Input;
using Wpf.Ui.Controls;
using MenuItem = Wpf.Ui.Controls.MenuItem;

namespace PocketLearn.Win
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : UiWindow
    {
        public static DependencyObject DepObject;
        public MainWindow()
        {
            Directory.CreateDirectory(ApplicationConstants.APPLICATION_DATA_PATH);
            Directory.CreateDirectory(Path.Combine(ApplicationConstants.APPLICATION_DATA_PATH, "Images"));

            if (File.Exists(Path.Combine(ApplicationConstants.APPLICATION_DATA_PATH, "latest.txt")))
                File.Delete(Path.Combine(ApplicationConstants.APPLICATION_DATA_PATH, "latest.txt"));

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Is(WinConfig.Get().LogLevel)
                .WriteTo.Console()
                .WriteTo.File(Path.Combine(ApplicationConstants.APPLICATION_DATA_PATH, "latest.txt"), outputTemplate: "{Timestamp:HH:mm:ss} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
                .CreateLogger();

            new MainWindowVM();
            InitializeComponent();
            DepObject = this;

            this.PreviewKeyDown += MainWindow_PreviewKeyDown;
        }

        private void MainWindow_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
            {
                e.Handled = true;
            }
        }

        private void TrayMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            if (sender is not MenuItem menuItem)
                return;

            switch (menuItem.Tag.ToString())
            {
                case "Open": Show(); WindowState = WindowState.Normal; break;
                case "Close": Application.Current.Shutdown(); break;
                default:
                    break;
            }
        }

        private void UiWindow_Closing(object sender, CancelEventArgs e)
        {
            if (Keyboard.GetKeyStates(Key.LeftShift) == KeyStates.Down)
            {
                Log.Debug("Shutdown application");
                ((App)Application.Current).Window_Closing(this, null);
                Application.Current.Shutdown();
                return;
            }
            e.Cancel = true;
            Application.Current.ShutdownMode = ShutdownMode.OnExplicitShutdown;
            WindowState = WindowState.Minimized;
            Hide();
            Log.Debug("Minimize application to tray");
        }
    }
}

using Microsoft.Toolkit.Uwp.Notifications;
using PocketLearn.Core;
using PocketLearn.Win.Core;
using PocketLearn.Win.Core.Notification;
using PocketLearn.Win.MVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
                Application.Current.Shutdown();
                return;
            }
            e.Cancel = true;
            Application.Current.ShutdownMode = ShutdownMode.OnExplicitShutdown;
            WindowState = WindowState.Minimized;
            Hide();
        }
    }
}

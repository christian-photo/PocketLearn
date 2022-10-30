using System.Windows;
using Wpf.Ui.Controls;
using PocketLearn.Win.Core;
using PocketLearn.Public.Core.Config;
using PocketLearn.Win.API;
using PocketLearn.Shared.Core.Learning;

namespace PocketLearn.Win.MVVM.PopUp
{
    /// <summary>
    /// Interaktionslogik für SyncPopUp.xaml
    /// </summary>
    public partial class SyncPopUp : UiWindow
    {
        private LearnProject syncProject;
        public SyncPopUp(LearnProject project)
        {
            InitializeComponent();
            syncProject = project;
        }

        private void Sync(object sender, RoutedEventArgs e)
        {
            bool syncImages = SyncImages.IsChecked.Value;
            if (!syncImages)
            {
                LearnProject tempProject = syncProject.MakeDeepCopy();
                foreach (LearnCard card in tempProject.Cards)
                {
                    card.CardContent1.Items.RemoveAll(x => x.Type == CardContentItemType.Image);
                    card.CardContent2.Items.RemoveAll(x => x.Type == CardContentItemType.Image);
                }
                APIHandler.ProjectToSync = tempProject;
                QrCode.Source = Utility.CreateQRCode($"http://{Utility.GetIPv4Address()}:{WinConfig.Get().Port}/api/GetProject?images=false").ToBitmapImage();
            }
            else
            {
                APIHandler.ProjectToSync = syncProject;
                QrCode.Source = Utility.CreateQRCode($"http://{Utility.GetIPv4Address()}:{WinConfig.Get().Port}/api/GetProject?images=true").ToBitmapImage();
            }
        }
    }
}

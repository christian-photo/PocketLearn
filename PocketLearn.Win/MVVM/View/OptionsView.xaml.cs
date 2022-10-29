using PocketLearn.Win.MVVM.ViewModel;
using System.Windows.Controls;

namespace PocketLearn.Win.MVVM.View
{
    /// <summary>
    /// Interaktionslogik für OptionsView.xaml
    /// </summary>
    public partial class OptionsView : UserControl
    {
        public OptionsView()
        {
            InitializeComponent();
        }

        private void UpdateProjectSettings(object sender, SelectionChangedEventArgs e)
        {
            MainWindowVM.Instance.OptionsVM.UpdateSettings();
        }

        private void Slider_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            MainWindowVM.Instance.OptionsVM.SettingsChanged();
        }
    }
}

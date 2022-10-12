using PocketLearn.Win.MVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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

        private void NotLearnedChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            MainWindowVM.Instance.OptionsVM.SettingsChanged();
        }
    }
}

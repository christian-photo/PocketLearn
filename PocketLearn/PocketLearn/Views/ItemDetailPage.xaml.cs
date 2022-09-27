using PocketLearn.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace PocketLearn.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}
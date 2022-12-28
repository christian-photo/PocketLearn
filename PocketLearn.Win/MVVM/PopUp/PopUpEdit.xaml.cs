using PocketLearn.Shared.Core.Learning;
using PocketLearn.Win.Core;
using PocketLearn.Win.MVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Wpf.Ui.Controls;
using Image = System.Windows.Controls.Image;
using Path = System.IO.Path;

namespace PocketLearn.Win.MVVM.PopUp
{
    /// <summary>
    /// Interaktionslogik für EditPopUp.xaml
    /// </summary>
    public partial class PopUpEdit : UiWindow
    {
        public LearnCard ActiveCard { get; set; }
        public LearnProject LearnProject { get; set; }

        public PopUpEdit(LearnProject learnProject, LearnCard learnCard)
        {
            ActiveCard = learnCard;
            LearnProject = learnProject;
            
            InitializeComponent();
            ObservableCollection<string> data = new ObservableCollection<string>();
            data.Add("twoway");
            data.Add("oneway");
            data.Add("reverse oneway");
            CardTypeCombo.ItemsSource = data;
            if(ActiveCard.CardType != null)
            {
                switch(ActiveCard.CardType)
                {
                    case CardType.TwoWay:
                        CardTypeCombo.SelectedIndex = 0;
                        break;
                    case CardType.OneWay:
                        CardTypeCombo.SelectedIndex=1;
                        break;
                    case CardType.ReverseOneWay:
                        CardTypeCombo.SelectedIndex=2;
                        break;

                }
            }
            ContainsLaTeX.IsChecked = learnCard.CardContent1.ContainsLaTeX;
            foreach (object obj in learnCard.CardContent1.Items)
            {
                CardContentItem item = (CardContentItem)obj;
                if (item.Type == CardContentItemType.Text)
                {
                    if (learnCard.CardContent1.Items.Last(x => x.Type == CardContentItemType.Text) == item)
                    {
                        QuestionText.Text += $"{item.Content}";
                    }
                    else
                    {
                        QuestionText.Text += $"{item.Content}\n";
                    }
                }
                else if (item.Type == CardContentItemType.Image)
                {
                    if (!File.Exists(Path.Combine(ApplicationConstants.APPLICATION_DATA_PATH, "Images", item.Content)))
                    {
                        learnCard.CardContent1.Items.Remove(item);
                    }
                    QuestionImages.Items.Add(new Image() { Source = new Bitmap(Path.Combine(ApplicationConstants.APPLICATION_DATA_PATH, "Images", item.Content)).ToBitmapImage() });
                }
            }
            foreach (object obj in learnCard.CardContent2.Items)
            {
                CardContentItem item = (CardContentItem)obj;
                if (item.Type == CardContentItemType.Text)
                {
                    if (learnCard.CardContent2.Items.Last(x => x.Type == CardContentItemType.Text) == item)
                    {
                        AnswerText.Text += $"{item.Content}";
                    }
                    else
                    {
                        AnswerText.Text += $"{item.Content}\n";
                    }
                }
                else if (item.Type == CardContentItemType.Image)
                {
                    if (!File.Exists(Path.Combine(ApplicationConstants.APPLICATION_DATA_PATH, "Images", item.Content)))
                    {
                        learnCard.CardContent2.Items.Remove(item);
                    }
                    AnswerImages.Items.Add(new Image() { Source = new Bitmap(Path.Combine(ApplicationConstants.APPLICATION_DATA_PATH, "Images", item.Content)).ToBitmapImage() });
                }
            }
        }

        private void AddImage(object sender, RoutedEventArgs e)
        {
            List<string> files = Utility.FileDialog("Images(*.jpg;*.bmp;*.png;*.tiff)|*.jpg;*.bmp;*.png;*.tiff", "Select images");
            if (files == null) return;
            foreach (string file in files)
            {
                QuestionImages.Items.Add(new Image() { Source = new Bitmap(file).ToBitmapImage() });
            }
        }

        private void AddImageAnswer(object sender, RoutedEventArgs e)
        {
            List<string> files = Utility.FileDialog("Images(*.jpg;*.bmp;*.png;*.tiff)|*.jpg;*.bmp;*.png;*.tiff", "Select images");
            if (files == null) return;
            foreach (string file in files)
            {
                AnswerImages.Items.Add(new Image() { Source = new Bitmap(file).ToBitmapImage() });
            }
        }

        private void Accept(object sender, RoutedEventArgs e)
        {
            string directory = Path.Combine(ApplicationConstants.APPLICATION_DATA_PATH, "Images");
            ActiveCard.CardContent1.ClearItems(directory);
            ActiveCard.CardContent2.ClearItems(directory);
            switch(CardTypeCombo.Text) {
                case "twoway":
                    ActiveCard.CardType = CardType.TwoWay;
                    break;
                case "oneway":
                    ActiveCard.CardType = CardType.OneWay;
                    break;
                case "reverse oneway":
                    ActiveCard.CardType = CardType.ReverseOneWay;
                    break;

            }
            foreach (string part in QuestionText.Text.Split('\n'))
            {
                ActiveCard.CardContent1.Items.Add(new CardContentItem(part, CardContentItemType.Text));
            }
            foreach (Image bmp in QuestionImages.Items)
            {
                Bitmap image = ((BitmapImage)bmp.Source).ToBitmap();
                Guid imageGuid = Guid.NewGuid();
                image.Save(Path.Combine(ApplicationConstants.APPLICATION_DATA_PATH, "Images", imageGuid.ToString() + ".jpg"), Utility.GetEncoder(ImageFormat.Jpeg), Utility.GetCompression());

                ActiveCard.CardContent1.Items.Add(new CardContentItem(imageGuid.ToString() + ".jpg", CardContentItemType.Image));
            }

            foreach (string part in AnswerText.Text.Split('\n'))
            {
                ActiveCard.CardContent2.Items.Add(new CardContentItem(part, CardContentItemType.Text));
            }
            foreach (Image bmp in AnswerImages.Items)
            {
                Bitmap image = ((BitmapImage)bmp.Source).ToBitmap();
                Guid imageGuid = Guid.NewGuid();
                if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);
                image.Save(Path.Combine(directory, imageGuid.ToString() + ".jpg"), Utility.GetEncoder(ImageFormat.Jpeg), Utility.GetCompression());

                ActiveCard.CardContent2.Items.Add(new CardContentItem(imageGuid.ToString() + ".jpg", CardContentItemType.Image));
            }
            ActiveCard.CardContent1.ContainsLaTeX = (bool)ContainsLaTeX.IsChecked;
            ActiveCard.CardContent2.ContainsLaTeX = (bool)ContainsLaTeX.IsChecked;

            ActiveCard.LastEdit = DateTime.Now;
            MainWindowVM.Instance.EditVM.UpdateView(LearnProject);
            if (!Directory.Exists(ApplicationConstants.APPLICATION_DATA_PATH))
            {
                Directory.CreateDirectory(ApplicationConstants.APPLICATION_DATA_PATH);
            }

            File.WriteAllText(Path.Combine(ApplicationConstants.APPLICATION_DATA_PATH, "Projects.json"), MainWindowVM.Instance.ProjectManager.Serialize());
            LearnProject.LastEdit = DateTime.Now;
            Close();
        }

        private void ComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }
    }
}
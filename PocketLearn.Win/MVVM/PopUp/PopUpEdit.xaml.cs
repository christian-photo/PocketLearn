using PocketLearn.Core.Learning;
using PocketLearn.Win.Core;
using PocketLearn.Win.MVVM.ViewModel;
using Swan;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
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
using System.Windows.Shapes;
using Image = System.Windows.Controls.Image;
using Path = System.IO.Path;

namespace PocketLearn.Win.MVVM.PopUp
{
    /// <summary>
    /// Interaktionslogik für EditPopUp.xaml
    /// </summary>
    public partial class PopUpEdit : Window
    {
        public LearnCard ActiveCard { get; set; }
        public LearnProject LearnProject { get; set; }

        public PopUpEdit(LearnProject learnProject, LearnCard learnCard)
        {
            ActiveCard = learnCard;
            LearnProject = learnProject;

            InitializeComponent();
            foreach (object obj in learnCard.CardContent1.Items)
            {
                CardContentItem item = (CardContentItem)obj;
                if (item.Type == CardContentItemType.Text)
                {
                    if (learnCard.CardContent1.Items.Where(x => ((CardContentItem)x).Type == CardContentItemType.Text).Last() == obj)
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
                    QuestionImages.Items.Add(new Image() { Source = new Bitmap(Path.Combine(ApplicationConstants.APPLICATION_DATA_PATH, "Images", item.Content)).ToBitmapImage() });
                }
            }
            foreach (object obj in learnCard.CardContent2.Items)
            {
                CardContentItem item = (CardContentItem)obj;
                if (item.Type == CardContentItemType.Text)
                {
                    if (learnCard.CardContent2.Items.Where(x => ((CardContentItem)x).Type == CardContentItemType.Text).Last() == obj)
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
                    AnswerImages.Items.Add(new Image() { Source = new Bitmap(Path.Combine(ApplicationConstants.APPLICATION_DATA_PATH, "Images", item.Content)).ToBitmapImage() });
                }
            }
        }

        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void AddImage(object sender, RoutedEventArgs e)
        {
            List<string> files = Utility.FileDialog("Images(*.jpg;*.bmp;*.png;*.tiff)|*.jpg;*.bmp;*.png;*.tiff", "Select images");
            foreach (string file in files)
            {
                QuestionImages.Items.Add(new Image() { Source = new Bitmap(file).ToBitmapImage() });
            }
        }

        private void AddImageAnswer(object sender, RoutedEventArgs e)
        {
            List<string> files = Utility.FileDialog("Images(*.jpg;*.bmp;*.png;*.tiff)|*.jpg;*.bmp;*.png;*.tiff", "Select images");
            foreach (string file in files)
            {
                AnswerImages.Items.Add(new Image() { Source = new Bitmap(file).ToBitmapImage() });
            }
        }

        private void Accept(object sender, RoutedEventArgs e)
        {
            string directory = System.IO.Path.Combine(ApplicationConstants.APPLICATION_DATA_PATH, "Images");
            ActiveCard.CardContent1.ClearItems(directory);
            ActiveCard.CardContent2.ClearItems(directory);
            foreach (string part in QuestionText.Text.Split('\n'))
            {
                ActiveCard.CardContent1.Items.Add(new CardContentItem(part, CardContentItemType.Text));
            }
            foreach (Image bmp in QuestionImages.Items)
            {
                Bitmap image = ((BitmapImage)bmp.Source).ToBitmap();
                Guid imageGuid = Guid.NewGuid();
                image.Save(System.IO.Path.Combine(ApplicationConstants.APPLICATION_DATA_PATH, "Images", imageGuid.ToString() + ".jpg"));

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
                image.Save(System.IO.Path.Combine(directory, imageGuid.ToString() + ".jpg"));

                ActiveCard.CardContent2.Items.Add(new CardContentItem(imageGuid.ToString() + ".jpg", CardContentItemType.Image));
            }
            MainWindowVM.Instance.EditVM.UpdateView(LearnProject);
            Close();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
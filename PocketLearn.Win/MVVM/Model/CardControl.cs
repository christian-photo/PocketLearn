using PocketLearn.Core.Learning;
using PocketLearn.Win.MVVM.PopUp;
using PocketLearn.Win.MVVM.ViewModel;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Xamarin.Forms;

namespace PocketLearn.Win.MVVM.Model
{
    /// <summary>
    /// Führen Sie die Schritte 1a oder 1b und anschließend Schritt 2 aus, um dieses benutzerdefinierte Steuerelement in einer XAML-Datei zu verwenden.
    ///
    /// Schritt 1a) Verwenden des benutzerdefinierten Steuerelements in einer XAML-Datei, die im aktuellen Projekt vorhanden ist.
    /// Fügen Sie dieses XmlNamespace-Attribut dem Stammelement der Markupdatei
    /// an der Stelle hinzu, an der es verwendet werden soll:
    ///
    ///     xmlns:MyNamespace="clr-namespace:PocketLearn.Win.MVVM.Model"
    ///
    ///
    /// Schritt 1b) Verwenden des benutzerdefinierten Steuerelements in einer XAML-Datei, die in einem anderen Projekt vorhanden ist.
    /// Fügen Sie dieses XmlNamespace-Attribut dem Stammelement der Markupdatei
    /// an der Stelle hinzu, an der es verwendet werden soll:
    ///
    ///     xmlns:MyNamespace="clr-namespace:PocketLearn.Win.MVVM.Model;assembly=PocketLearn.Win.MVVM.Model"
    ///
    /// Darüber hinaus müssen Sie von dem Projekt, das die XAML-Datei enthält, einen Projektverweis
    /// zu diesem Projekt hinzufügen und das Projekt neu erstellen, um Kompilierungsfehler zu vermeiden:
    ///
    ///     Klicken Sie im Projektmappen-Explorer mit der rechten Maustaste auf das Zielprojekt und anschließend auf
    ///     "Verweis hinzufügen"->"Projekte"->[Navigieren Sie zu diesem Projekt, und wählen Sie es aus.]
    ///
    ///
    /// Schritt 2)
    /// Fahren Sie fort, und verwenden Sie das Steuerelement in der XAML-Datei.
    ///
    ///     <MyNamespace:LearningProjectControl/>
    ///
    /// </summary>
    public class CardControl : Control
    {

        public static readonly DependencyProperty EditProperty = DependencyProperty.Register(nameof(Edit), typeof(ICommand), typeof(CardControl), new UIPropertyMetadata(null));
        public ICommand Edit
        {
            get
            {
                return (ICommand)GetValue(EditProperty);
            }
            set
            {
                SetValue(EditProperty, value);
            }
        }

        public static readonly DependencyProperty DeleteProperty = DependencyProperty.Register(nameof(Delete), typeof(ICommand), typeof(CardControl), new UIPropertyMetadata(null));
        public ICommand Delete
        {
            get
            {
                return (ICommand)GetValue(DeleteProperty);
            }
            set
            {
                SetValue(EditProperty, value);
            }
        }

        public static readonly DependencyProperty CardContent1Property = DependencyProperty.Register(nameof(CardContent1), typeof(CardContent), typeof(CardControl), new UIPropertyMetadata(null));
        public CardContent CardContent1
        {
            get
            {
                return (CardContent)GetValue(CardContent1Property);
            }
            set
            {
                SetValue(CardContent1Property, value);
            }
        }

        public static readonly DependencyProperty CardContent2Property = DependencyProperty.Register(nameof(CardContent2), typeof(CardContent), typeof(CardControl), new UIPropertyMetadata(null));
        public CardContent CardContent2
        {
            get
            {
                return (CardContent)GetValue(CardContent2Property);
            }
            set
            {
                SetValue(CardContent2Property, value);
            }
        }

        public static readonly DependencyProperty DifficultyProperty = DependencyProperty.Register(nameof(Difficulty), typeof(CardDifficulty), typeof(CardControl), new UIPropertyMetadata(null));
        public CardDifficulty Difficulty
        {
            get
            {
                return (CardDifficulty)GetValue(DifficultyProperty);
            }
            set
            {
                SetValue(DifficultyProperty, value);
            }
        }

        public static readonly DependencyProperty LastLearnedTimeProperty = DependencyProperty.Register(nameof(LastLearned), typeof(DateTime), typeof(CardControl), new UIPropertyMetadata(null));
        public DateTime LastLearned
        {
            get
            {
                return (DateTime)GetValue(LastLearnedTimeProperty);
            }
            set
            {
                SetValue(LastLearnedTimeProperty, value);
            }
        }

        static CardControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CardControl), new FrameworkPropertyMetadata(typeof(CardControl)));
        }

        public CardControl(LearnCard card)
        {
            CardContent1 = card.CardContent1;
            CardContent2 = card.CardContent2;
            Difficulty = card.Difficulty;
            LastLearned = card.LastLearnedTime;

            Edit = new RelayCommand(_ =>
            {
                new PopUpEdit(card).Show();
            });
            Delete = new RelayCommand(_ =>
            {
                throw new NotImplementedException();
            });
        }
    }
}
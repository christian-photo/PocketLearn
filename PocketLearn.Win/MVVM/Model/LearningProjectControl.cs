using PocketLearn.Core.Learning;
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
    public class LearningProjectControl : Control
    {
        private Guid UUID;

        public static readonly DependencyProperty LearnProperty = DependencyProperty.Register(nameof(Learn), typeof(ICommand), typeof(LearningProjectControl), new UIPropertyMetadata(null));
        public ICommand Learn
        {
            get
            {
                return (ICommand)GetValue(LearnProperty);
            }
            set
            {
                SetValue(LearnProperty, value);
            }
        }

        public static readonly DependencyProperty EditProperty = DependencyProperty.Register(nameof(Edit), typeof(ICommand), typeof(LearningProjectControl), new UIPropertyMetadata(null));
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

        public static readonly DependencyProperty ProjectNameProperty = DependencyProperty.Register(nameof(ProjectName), typeof(string), typeof(LearningProjectControl), new UIPropertyMetadata(null));
        public string ProjectName
        {
            get
            {
                return (string)GetValue(ProjectNameProperty);
            }
            set
            {
                SetValue(ProjectNameProperty, value);
            }
        }

        public static readonly DependencyProperty CreationTimeProperty = DependencyProperty.Register(nameof(CreationTime), typeof(DateTime), typeof(LearningProjectControl), new UIPropertyMetadata(null));
        public DateTime CreationTime
        {
            get
            {
                return (DateTime)GetValue(CreationTimeProperty);
            }
            set
            {
                SetValue(CreationTimeProperty, value);
            }
        }

        public static readonly DependencyProperty HasToBeCompletedProperty = DependencyProperty.Register(nameof(HasToBeCompleted), typeof(DateTime), typeof(LearningProjectControl), new UIPropertyMetadata(null));
        public DateTime HasToBeCompleted
        {
            get
            {
                return (DateTime)GetValue(HasToBeCompletedProperty);
            }
            set
            {
                SetValue(HasToBeCompletedProperty, value);
            }
        }

        public static readonly DependencyProperty ShouldLearnProperty = DependencyProperty.Register(nameof(ShouldLearn), typeof(bool), typeof(LearningProjectControl), new UIPropertyMetadata(null));
        public bool ShouldLearn
        {
            get
            {
                return (bool)GetValue(ShouldLearnProperty);
            }
            set
            {
                SetValue(ShouldLearnProperty, value);
            }
        }

        static LearningProjectControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(LearningProjectControl), new FrameworkPropertyMetadata(typeof(LearningProjectControl)));
        }

        public LearningProjectControl(LearnProject project)
        {
            ProjectName = project.ProjectName;
            CreationTime = project.CreationTime;
            HasToBeCompleted = project.HasToBeCompleted;
            ShouldLearn = project.ShouldLearn();


            Learn = new RelayCommand(_ =>
            {
                throw new NotImplementedException();
            });
            Edit = new RelayCommand(_ =>
            {
                throw new NotImplementedException();
            });
            UUID = projectID;
        }
    }
}
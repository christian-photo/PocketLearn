using PocketLearn.Shared.Core.Learning;
using PocketLearn.Win.Core;
using PocketLearn.Win.MVVM.PopUp;
using PocketLearn.Win.MVVM.ViewModel;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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

        public static readonly DependencyProperty SyncProperty = DependencyProperty.Register(nameof(Sync), typeof(ICommand), typeof(LearningProjectControl), new UIPropertyMetadata(null));
        public ICommand Sync
        {
            get
            {
                return (ICommand)GetValue(SyncProperty);
            }
            set
            {
                SetValue(SyncProperty, value);
            }
        }

        public static readonly DependencyProperty DeleteProperty = DependencyProperty.Register(nameof(Delete), typeof(ICommand), typeof(LearningProjectControl), new UIPropertyMetadata(null));
        public ICommand Delete
        {
            get
            {
                return (ICommand)GetValue(DeleteProperty);
            }
            set
            {
                SetValue(DeleteProperty, value);
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

        public static readonly DependencyProperty SubjectProperty = DependencyProperty.Register(nameof(Subject), typeof(LearnSubject), typeof(LearningProjectControl), new UIPropertyMetadata(null));
        public LearnSubject Subject
        {
            get
            {
                return (LearnSubject)GetValue(SubjectProperty);
            }
            set
            {
                SetValue(SubjectProperty, value);
            }
        }

        static LearningProjectControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(LearningProjectControl), new FrameworkPropertyMetadata(typeof(LearningProjectControl)));
        }

        public LearningProjectControl(LearnProject project, ProjectManager manager)
        {
            ProjectName = project.ProjectName;
            CreationTime = project.CreationTime;
            HasToBeCompleted = project.HasToBeCompleted;
            ShouldLearn = project.ShouldLearn();
            Subject = project.LearnSubject;

            Learn = new RelayCommand(_ =>
            {
                MainWindowVM.Instance.QuestionVM = new QuestionVM(project);
                MainWindowVM.Instance.AnswerVM = new AnswerVM(project);

                Utility.NavigateToPage(ApplicationConstants.QuestionViewURI);
            });
            Edit = new RelayCommand(_ =>
            {
                MainWindowVM.Instance.EditVM.UpdateView(project);
                Utility.NavigateToPage(ApplicationConstants.EditViewURI);
            });
            Sync = new RelayCommand(_ =>
            {
                new SyncPopUp(project).ShowDialog();
            });
            Delete = new RelayCommand(_ =>
            {
                new DeleteProjectPopUp(project, manager).ShowDialog();
            });
            UUID = project.ProjectID;
        }
    }
}
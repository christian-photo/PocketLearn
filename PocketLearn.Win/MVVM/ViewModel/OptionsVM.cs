using PocketLearn.Core.Learning;
using System.Collections.Generic;
using System.Linq;

namespace PocketLearn.Win.MVVM.ViewModel
{
    public class OptionsVM : ObservableObject
    {
        private List<string> _projects;
        public List<string> Projects
        {
            get => _projects;
            set
            {
                _projects = value;
                RaisePropertyChanged();
            }
        }

        private int _index = 0;
        public int Index
        {
            get => _index;
            set
            {
                _index = value;
                RaisePropertyChanged();
            }
        }

        private float notLearnedFactor;
        public float NotLearnedFactor
        {
            get => notLearnedFactor;
            set
            {
                notLearnedFactor = value;
                RaisePropertyChanged();
            }
        }

        private float easyFactor;
        public float EasyFactor
        {
            get => easyFactor;
            set
            {
                easyFactor = value;
                RaisePropertyChanged();
            }
        }

        private float okFactor;
        public float OKFactor
        {
            get => okFactor;
            set
            {
                okFactor = value;
                RaisePropertyChanged();
            }
        }

        private float mediumFactor;
        public float MediumFactor
        {
            get => mediumFactor;
            set
            {
                mediumFactor = value;
                RaisePropertyChanged();
            }
        }

        private float hardFactor;
        public float HardFactor
        {
            get => hardFactor;
            set
            {
                hardFactor = value;
                RaisePropertyChanged();
            }
        }

        public ProjectManager Manager;

        private LearnProject active;


        public OptionsVM(ProjectManager manager)
        {
            Manager = manager;
            List<string> t = new();
            foreach (LearnProject proj in manager.LearnProjects)
            {
                t.Add(proj.ProjectName);
            }
            Projects = t;
            UpdateSettings();
        }

        public void UpdateSettings()
        {
            if (active is not null)
            {
                SettingsChanged();
                Manager.LearnProjects.RemoveAll(proj => proj.ProjectID == active.ProjectID);
                Manager.LearnProjects.Add(active);
            }
            active = Manager.LearnProjects.Where(x => x.ProjectName == Projects[Index]).First();
            NotLearnedFactor = active.ProjectConfig.NotLearnedFactor;
            EasyFactor = active.ProjectConfig.EasyFactor;
            OKFactor = active.ProjectConfig.OKFactor;
            MediumFactor = active.ProjectConfig.MediumFactor;
            HardFactor = active.ProjectConfig.HardFactor;
        }

        public void SettingsChanged()
        {
            active.ProjectConfig.NotLearnedFactor = NotLearnedFactor;
            active.ProjectConfig.EasyFactor = EasyFactor;
            active.ProjectConfig.OKFactor = OKFactor;
            active.ProjectConfig.MediumFactor = MediumFactor;
            active.ProjectConfig.HardFactor = HardFactor;
        }
    }
}

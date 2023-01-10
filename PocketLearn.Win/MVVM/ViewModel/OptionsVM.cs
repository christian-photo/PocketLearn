#region "copyright"

/*
    Copyright © 2023 Christian Palm (christian@palm-family.de)
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at http://mozilla.org/MPL/2.0/.
*/

#endregion "copyright"

using PocketLearn.Public.Core.Config;
using PocketLearn.Shared.Core.Learning;
using PocketLearn.Win.MVVM.Model;
using PocketLearn.Win.MVVM.PopUp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace PocketLearn.Win.MVVM.ViewModel
{
    public class OptionsVM : ObservableObject
    {

        private List<LearnTimeControl> _learnTimesView;
        public List<LearnTimeControl> LearnTimesView
        {
            get => _learnTimesView;
            set
            {
                _learnTimesView = value;
                RaisePropertyChanged();
            }
        }

        public RelayCommand AddLearnTime { get; private set; }

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

        public RelayCommand DonateCoffee { get; set; }

        public ProjectManager Manager;

        private LearnProject active;

        private ProjectConfig activeConfig;


        public OptionsVM(ProjectManager manager)
        {
            Manager = manager;

            UpdateList(this);

            if (Projects.Count > 0)
                UpdateSettings();

            Manager.ProjectsChanged += UpdateList;

            AddLearnTime = new RelayCommand(_ =>
            {
                new LearnTimePopUp(manager).ShowDialog();
            });

            DonateCoffee = new RelayCommand(_ =>
            {
                return; // TODO: Make paypal address/account
                string url = "";

                string business = "my@paypalemail.com";  // your paypal email
                string description = "Donation";            // '%20' represents a space. remember HTML!
                string country = "DE";                  // AU, US, etc.
                string currency = "EUR";                 // AUD, USD, etc.

                // &amount=1.99 for preset amount of 1.99€

                url += "https://www.paypal.com/cgi-bin/webscr" +
                    "?cmd=" + "_donations" +
                    "&business=" + business +
                    "&lc=" + country +
                    "&item_name=" + description +
                    "&currency_code=" + currency +
                    "&bn=" + "PP%2dDonationsBF";

                Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
            });
        }

        private void UpdateList(object sender)
        {
            List<string> t = new();
            foreach (LearnProject proj in Manager.LearnProjects)
            {
                t.Add(proj.ProjectName);
            }
            Projects = t;
            Index = 0;
            List<LearnTimeControl> l = new();
            foreach (var lt in WinConfig.Get().LearnTimes)
            {
                l.Add(new LearnTimeControl(lt.Item1, lt.Item2));
            }
            LearnTimesView = l;
        }

        public void UpdateSettings()
        {
            if (active is not null)
            {
                SettingsChanged();
                active.ProjectConfig = activeConfig;
            }
            active = Manager.LearnProjects.Where(x => x.ProjectName == Projects[Index]).FirstOrDefault();
            if (active is null)
            {
                NotLearnedFactor = 0;
                EasyFactor = 0;
                OKFactor = 0;
                MediumFactor = 0;
                HardFactor = 0;
                return;
            }
            activeConfig = new ProjectConfig()
            {
                EasyFactor = active.ProjectConfig.EasyFactor,
                HardFactor = active.ProjectConfig.HardFactor,
                MediumFactor = active.ProjectConfig.MediumFactor,
                NotLearnedFactor = active.ProjectConfig.OKFactor,
                OKFactor = active.ProjectConfig.OKFactor
            };
            NotLearnedFactor = activeConfig.NotLearnedFactor;
            EasyFactor = activeConfig.EasyFactor;
            OKFactor = activeConfig.OKFactor;
            MediumFactor = activeConfig.MediumFactor;
            HardFactor = activeConfig.HardFactor;
        }

        public void SettingsChanged()
        {
            if (active is null) return;
            activeConfig.NotLearnedFactor = NotLearnedFactor;
            activeConfig.EasyFactor = EasyFactor;
            activeConfig.OKFactor = OKFactor;
            activeConfig.MediumFactor = MediumFactor;
            activeConfig.HardFactor = HardFactor;
            activeConfig.LastEdit = DateTime.Now;
        }
    }
}

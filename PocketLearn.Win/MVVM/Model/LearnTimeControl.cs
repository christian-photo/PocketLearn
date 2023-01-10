#region "copyright"

/*
    Copyright © 2023 Christian Palm (christian@palm-family.de)
    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at http://mozilla.org/MPL/2.0/.
*/

#endregion "copyright"

using PocketLearn.Shared.Core.Learning;
using PocketLearn.Win.MVVM.ViewModel;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PocketLearn.Win.MVVM.Model
{
    public class LearnTimeControl : Control
    {

        public static readonly DependencyProperty DeleteProperty = DependencyProperty.Register(nameof(Delete), typeof(ICommand), typeof(LearnTimeControl), new UIPropertyMetadata(null));
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

        public static readonly DependencyProperty FromMinutesProperty = DependencyProperty.Register(nameof(FromMinutes), typeof(int), typeof(LearnTimeControl), new UIPropertyMetadata(null));
        public int FromMinutes
        {
            get
            {
                return (int)GetValue(FromMinutesProperty);
            }
            set
            {
                SetValue(FromMinutesProperty, value);
            }
        }

        public static readonly DependencyProperty ToMinutesProperty = DependencyProperty.Register(nameof(ToMinutes), typeof(int), typeof(LearnTimeControl), new UIPropertyMetadata(null));
        public int ToMinutes
        {
            get
            {
                return (int)GetValue(ToMinutesProperty);
            }
            set
            {
                SetValue(ToMinutesProperty, value);
            }
        }



        public static readonly DependencyProperty FromHoursProperty = DependencyProperty.Register(nameof(FromHours), typeof(int), typeof(LearnTimeControl), new UIPropertyMetadata(null));
        public int FromHours
        {
            get
            {
                return (int)GetValue(FromHoursProperty);
            }
            set
            {
                SetValue(FromHoursProperty, value);
            }
        }

        public static readonly DependencyProperty ToHoursProperty = DependencyProperty.Register(nameof(ToHours), typeof(int), typeof(LearnTimeControl), new UIPropertyMetadata(null));
        public int ToHours
        {
            get
            {
                return (int)GetValue(ToHoursProperty);
            }
            set
            {
                SetValue(ToHoursProperty, value);
            }
        }


        static LearnTimeControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(LearnTimeControl), new FrameworkPropertyMetadata(typeof(LearnTimeControl)));
        }

        public LearnTimeControl(TimeSpan from, TimeSpan to)
        {
            FromMinutes = from.Minutes;
            FromHours = from.Hours;

            ToMinutes = to.Minutes;
            ToHours = to.Hours;

            Delete = new RelayCommand(_ =>
            {
                MainWindowVM.Instance.ProjectManager.RemoveLearnTime(from, to);
            });
        }
    }
    
}

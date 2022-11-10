
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PocketLearn.Win.MVVM.Model
{/*
    public class LearnTimeControl : Control
    {

        public static readonly DependencyProperty DeleteProperty = DependencyProperty.Register(nameof(Delete), typeof(ICommand), typeof(CardControl), new UIPropertyMetadata(null));
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

        static LearnTimeControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CardControl), new FrameworkPropertyMetadata(typeof(CardControl)));
        }

        public LearnTimeControl(TimeSpan from, TimeSpan to)
        {
         //   FromMinutes = from.Minutes;
           // FromHours = from.Hours;

            //ToMinutes = to.Minutes;
            //ToHours = to.Hours;

            Delete = new RelayCommand(_ =>
            {
            });
        }
    }
    */
}

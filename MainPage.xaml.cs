using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;

namespace Eire_Guess
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            //IsolatedStorageFile
        }

        private void playButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Play.xaml", UriKind.RelativeOrAbsolute));
        }

        private void ScoresButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Scores.xaml", UriKind.RelativeOrAbsolute));
        }

        private void goldPot_Tap(object sender, GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Merits.xaml", UriKind.RelativeOrAbsolute));
        }

        //private void cog_Tap(object sender, GestureEventArgs e)
        //{
        //    NavigationService.Navigate(new Uri("/Settings.xaml", UriKind.RelativeOrAbsolute));
        //}

        private void settingsIcon_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Settings.xaml", UriKind.RelativeOrAbsolute));
        }

        private void aboutIcon_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/About.xaml", UriKind.RelativeOrAbsolute));
        }

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            if (NavigationService.CanGoBack)
            {
                while (NavigationService.RemoveBackEntry() != null)
                {
                    NavigationService.RemoveBackEntry();
                }
            }
        }
    }
}
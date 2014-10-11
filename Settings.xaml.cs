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
//using System.IO;
using System.IO.IsolatedStorage;

namespace Eire_Guess
{
    public partial class Settings : PhoneApplicationPage
    {


        // create a directory within this page of the app
        //private IsolatedStorageSettings userSettings = IsolatedStorageSettings.ApplicationSettings;

        public Settings()
        {
            InitializeComponent();

            #region app.xaml variable
            /*App thisApp = Application.Current as App;
            string tempName = thisApp._userName;*/
            #endregion

            #region global var class
            //string temp = GlobalVars.tempName;

            // creates a directory (if already created then ignores)
            //settingsStorage.CreateDirectory("myData");

            // creates file
            //settingsStorage.CreateFile("userName");
            #endregion

        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            #region using app.xaml variables
            /*
            //if ((App.Current as App)._userName == null)
            // try is shorter & simpler
            try
            {
                nameBox.Text = (App.Current as App)._userName;
            }
            //else
            catch
            {
                nameBox.Text = "John";
            }

            //if ((App.Current as App)._sound == null)
            try
            {
                soundButton.Content = (App.Current as App)._sound;              
            }
            //else
            catch
            {
                soundButton.Content = "On";
            }

            //if ((App.Current as App)._sound == null)
            try
            {
                musicButton.Content = (App.Current as App)._music;
            }
            //else
            catch
            {
                musicButton.Content = "On";
            }          
             */
            #endregion

            nameBox.Text = GlobalVars._userName;
            if (GlobalVars._timerOn == true)
            {
                timerButton.Content = "On";
            }
            else
            {
                timerButton.Content = "Off";
            }

            //try
            //{
            //    nameBox.Text = (String)userSettings["name"];
            //    soundButton.Content = (String)userSettings["sound"];
            //    musicButton.Content = (String)userSettings["music"];
            //}
            //catch
            //{
            //    nameBox.Text = "Ronan";
            //    soundButton.Content = "On";
            //    musicButton.Content = "On";
            //}

            
        }

        protected override void OnNavigatedFrom(System.Windows.Navigation.NavigationEventArgs e)
        {
            GlobalVars._userName = nameBox.Text;

            //try
            //{
            //    GlobalVars._userName = nameBox.Text;



            //    userSettings["name"] = nameBox.Text;
            //    userSettings["sound"] = soundButton.Content;
            //    userSettings["music"] = musicButton.Content;
            //}
            //catch
            //{
            //    userSettings.Add("name", nameBox.Text);
            //    userSettings.Add("sound", soundButton.Content);
            //    userSettings.Add("music", musicButton.Content);
            //}
        }

        private void timerButton_Click(object sender, RoutedEventArgs e)
        {
            if (timerButton.Content.Equals("On"))
            {
                //timerButton.IsEnabled = false;
                GlobalVars._timerOn = false;
                timerButton.Content = "Off";
            }
            else
            {
                //timerButton.IsEnabled = true;
                GlobalVars._timerOn = true;
                timerButton.Content = "On";
            }
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/MainPage.xaml",UriKind.RelativeOrAbsolute));
        }

        //private void nameBox_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    GlobalVars._userName = nameBox.Text;
        //    // get focus event:
        //    //nameBox.Select(0, nameBox.Text.Length);
        //    //(App.Current as App)._userName = nameBox.Text;
        //}

        //private void soundButton_Click(object sender, RoutedEventArgs e)
        //{
        //    if (soundButton.Content.Equals("On"))
        //    {
        //        soundButton.Content = "Off";
        //        //(App.Current as App)._sound = "Off";
        //    }
        //    else
        //    {
        //        soundButton.Content = "On";
        //        //(App.Current as App)._sound = "On";
        //    }
        //}

        //private void musicButton_Click(object sender, RoutedEventArgs e)
        //{
        //    if (musicButton.Content.Equals("On"))
        //    {
        //        musicButton.Content = "Off";
        //        //(App.Current as App)._music = "Off";
        //    }
        //    else
        //    {
        //        musicButton.Content = "On";
        //        //(App.Current as App)._music = "On";
        //    }
        //}
    }
}
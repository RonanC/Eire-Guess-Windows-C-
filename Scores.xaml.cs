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
using System.IO.IsolatedStorage;

namespace Eire_Guess
{
    public partial class Scores : PhoneApplicationPage
    {
        public IsolatedStorageSettings scoreSettings = IsolatedStorageSettings.ApplicationSettings;

        public Scores()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            try
            {
                name1.Text = GlobalVars._scoreNames[0];
                score1.Text = GlobalVars._scoreNumbers[0].ToString();

                name1.Text = GlobalVars._scoreNames[0];
                score1.Text = GlobalVars._scoreNumbers[0].ToString();

                name2.Text = GlobalVars._scoreNames[1];
                score2.Text = GlobalVars._scoreNumbers[1].ToString();

                name3.Text = GlobalVars._scoreNames[2];
                score3.Text = GlobalVars._scoreNumbers[2].ToString();

                name4.Text = GlobalVars._scoreNames[3];
                score4.Text = GlobalVars._scoreNumbers[3].ToString();

                name5.Text = GlobalVars._scoreNames[4];
                score5.Text = GlobalVars._scoreNumbers[4].ToString();

                #region storageSettings
                /*name1.Text = (String)scoreSettings["name1"];
                score1.Text = (String)scoreSettings["score1"];

                name1.Text = (String)scoreSettings["name2"];
                score1.Text = (String)scoreSettings["score2"];

                name1.Text = (String)scoreSettings["name3"];
                score1.Text = (String)scoreSettings["score3"];

                name1.Text = (String)scoreSettings["name4"];
                score1.Text = (String)scoreSettings["score4"];

                name1.Text = (String)scoreSettings["name5"];
                score1.Text = (String)scoreSettings["score5"];*/
                #endregion
            }
            catch
            {
                name1.Text = " ";
                score1.Text = " ";

                name2.Text = " ";
                score2.Text = " ";

                name3.Text = " ";
                score3.Text = " ";

                name4.Text = " ";
                score4.Text = " ";

                name5.Text = " ";
                score5.Text = " ";
            }
        }

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            NavigationService.Navigate(new Uri("/MainPage.xaml",UriKind.RelativeOrAbsolute));
        }
    }
}
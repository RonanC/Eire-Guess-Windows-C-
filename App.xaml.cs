// Ronan Connolly - 13th Nov 2013 (finished prototype)
// Eire Guess - Irish County Guessing Game

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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.IO.IsolatedStorage;

namespace Eire_Guess
{
    // app class:
    public partial class App : Application
    {
        // Create in App.XAML!

        // create a directory within this page of the app
        private IsolatedStorageSettings appSettings = IsolatedStorageSettings.ApplicationSettings;
        private IsolatedStorageSettings scoreSettings = IsolatedStorageSettings.ApplicationSettings;


        // app class members (global variables):
        /*public string _userName { get; set; }
        public string _sound { get; set; }
        public string _music { get; set; }*/


        /// <summary>
        /// Provides easy access to the root frame of the Phone Application.
        /// </summary>
        /// <returns>The root frame of the Phone Application.</returns>
        /// 
        public PhoneApplicationFrame RootFrame { get; private set; }

        /// <summary>
        /// Constructor for the Application object.
        /// </summary>
        public App()
        {
            // Global handler for uncaught exceptions. 
            UnhandledException += Application_UnhandledException;

            // Standard Silverlight initialization
            InitializeComponent();

            // Phone-specific initialization
            InitializePhoneApplication();

            // Show graphics profiling information while debugging.
            if (System.Diagnostics.Debugger.IsAttached)
            {
                // Display the current frame rate counters.
                Application.Current.Host.Settings.EnableFrameRateCounter = true;

                // Show the areas of the app that are being redrawn in each frame.
                //Application.Current.Host.Settings.EnableRedrawRegions = true;

                // Enable non-production analysis visualization mode, 
                // which shows areas of a page that are handed off to GPU with a colored overlay.
                //Application.Current.Host.Settings.EnableCacheVisualization = true;

                // Disable the application idle detection by setting the UserIdleDetectionMode property of the
                // application's PhoneApplicationService object to Disabled.
                // Caution:- Use this under debug mode only. Application that disables user idle detection will continue to run
                // and consume battery power when the user is not using the phone.
                PhoneApplicationService.Current.UserIdleDetectionMode = IdleDetectionMode.Disabled;
            }

        }

        // Code to execute when the application is launching (eg, from Start)
        // This code will not execute when the application is reactivated
        private void Application_Launching(object sender, LaunchingEventArgs e)
        {
            //LoadSettings(){}

            #region userName
            try
            {
                GlobalVars._userName = (String)appSettings["userName"];

            }
            catch
            {
                GlobalVars._userName = "Paddy";
            }
            #endregion  

            #region scores

            try
            {
                GlobalVars._scoreNames[0] = (String)scoreSettings["name1"];
                GlobalVars._scoreNumbers[0] = (int)scoreSettings["score1"];

                GlobalVars._scoreNames[1] = (String)scoreSettings["name2"];
                GlobalVars._scoreNumbers[1] = (int)scoreSettings["score2"];

                GlobalVars._scoreNames[2] = (String)scoreSettings["name3"];
                GlobalVars._scoreNumbers[2] = (int)scoreSettings["score3"];

                GlobalVars._scoreNames[3] = (String)scoreSettings["name4"];
                GlobalVars._scoreNumbers[3] = (int)scoreSettings["score4"];

                GlobalVars._scoreNames[4] = (String)scoreSettings["name5"];
                GlobalVars._scoreNumbers[4] = (int)scoreSettings["score5"];
            }
            catch
            {
                GlobalVars._scoreNames[0] = " ";
                GlobalVars._scoreNumbers[0] = 0;

                GlobalVars._scoreNames[1] = " ";
                GlobalVars._scoreNumbers[1] = 0;

                GlobalVars._scoreNames[2] = " ";
                GlobalVars._scoreNumbers[2] = 0;

                GlobalVars._scoreNames[3] = " ";
                GlobalVars._scoreNumbers[3] = 0;

                GlobalVars._scoreNames[4] = " ";
                GlobalVars._scoreNumbers[4] = 0;
            }

            #endregion

            #region timer
            try
            {
                GlobalVars._timerOn = (bool)appSettings["timerOn"];

            }
            catch
            {
                GlobalVars._timerOn = true;
            }
            #endregion
        }

        // Code to execute when the application is activated (brought to foreground)
        // This code will not execute when the application is first launched
        private void Application_Activated(object sender, ActivatedEventArgs e)
        {
            #region userName
            GlobalVars._userName = (String)appSettings["userName"];
            #endregion  

            #region scores
            GlobalVars._scoreNames[0] = (String)scoreSettings["name1"];
            GlobalVars._scoreNumbers[0] = (int)scoreSettings["score1"];

            GlobalVars._scoreNames[1] = (String)scoreSettings["name2"];
            GlobalVars._scoreNumbers[1] = (int)scoreSettings["score2"];

            GlobalVars._scoreNames[2] = (String)scoreSettings["name3"];
            GlobalVars._scoreNumbers[2] = (int)scoreSettings["score3"];

            GlobalVars._scoreNames[3] = (String)scoreSettings["name4"];
            GlobalVars._scoreNumbers[3] = (int)scoreSettings["score4"];

            GlobalVars._scoreNames[4] = (String)scoreSettings["name5"];
            GlobalVars._scoreNumbers[4] = (int)scoreSettings["score5"];
            #endregion

            #region timer
            GlobalVars._timerOn = (bool)appSettings["timerOn"];
            #endregion

        }

        // Code to execute when the application is deactivated (sent to background)
        // This code will not execute when the application is closing
        private void Application_Deactivated(object sender, DeactivatedEventArgs e)
        {
            #region userName
            try
            {
                appSettings["userName"] = GlobalVars._userName;

            }
            catch
            {
                appSettings.Add("userName", GlobalVars._userName);
            }
            #endregion

            #region scores

            try
            {
                scoreSettings["name1"] = GlobalVars._scoreNames[0];
                scoreSettings["score1"] = GlobalVars._scoreNumbers[0];

                scoreSettings["name2"] = GlobalVars._scoreNames[1];
                scoreSettings["score2"] = GlobalVars._scoreNumbers[1];

                scoreSettings["name3"] = GlobalVars._scoreNames[2];
                scoreSettings["score3"] = GlobalVars._scoreNumbers[2];

                scoreSettings["name4"] = GlobalVars._scoreNames[3];
                scoreSettings["score4"] = GlobalVars._scoreNumbers[3];

                scoreSettings["name5"] = GlobalVars._scoreNames[4];
                scoreSettings["score5"] = GlobalVars._scoreNumbers[4];

            }
            catch
            {
                scoreSettings.Add("name1", GlobalVars._scoreNames[0]);
                scoreSettings.Add("score1", GlobalVars._scoreNumbers[0]);

                scoreSettings.Add("name2", GlobalVars._scoreNames[1]);
                scoreSettings.Add("score2", GlobalVars._scoreNumbers[1]);

                scoreSettings.Add("name3", GlobalVars._scoreNames[2]);
                scoreSettings.Add("score3", GlobalVars._scoreNumbers[2]);

                scoreSettings.Add("name4", GlobalVars._scoreNames[3]);
                scoreSettings.Add("score4", GlobalVars._scoreNumbers[3]);

                scoreSettings.Add("name5", GlobalVars._scoreNames[4]);
                scoreSettings.Add("score5", GlobalVars._scoreNumbers[4]);

            }
            #endregion

            #region timer
            appSettings["timerOn"] = GlobalVars._timerOn;
            #endregion
        }

        // Code to execute when the application is closing (eg, user hit Back)
        // This code will not execute when the application is deactivated
        private void Application_Closing(object sender, ClosingEventArgs e)
        {
            //void SaveSettings(){}

            #region userName
            try
            {
                appSettings["userName"] = GlobalVars._userName;

            }
            catch
            {
                appSettings.Add("userName", GlobalVars._userName);
            }
            #endregion

            #region scores

            try
            {
                scoreSettings["name1"] = GlobalVars._scoreNames[0];
                scoreSettings["score1"] = GlobalVars._scoreNumbers[0];

                scoreSettings["name2"] = GlobalVars._scoreNames[1];
                scoreSettings["score2"] = GlobalVars._scoreNumbers[1];

                scoreSettings["name3"] = GlobalVars._scoreNames[2];
                scoreSettings["score3"] = GlobalVars._scoreNumbers[2];

                scoreSettings["name4"] = GlobalVars._scoreNames[3];
                scoreSettings["score4"] = GlobalVars._scoreNumbers[3];

                scoreSettings["name5"] = GlobalVars._scoreNames[4];
                scoreSettings["score5"] = GlobalVars._scoreNumbers[4];
            }
            catch
            {
                scoreSettings.Add("name1", GlobalVars._scoreNames[0]);
                scoreSettings.Add("score1", GlobalVars._scoreNumbers[0]);

                scoreSettings.Add("name2", GlobalVars._scoreNames[1]);
                scoreSettings.Add("score2", GlobalVars._scoreNumbers[1]);

                scoreSettings.Add("name3", GlobalVars._scoreNames[2]);
                scoreSettings.Add("score3", GlobalVars._scoreNumbers[2]);

                scoreSettings.Add("name4", GlobalVars._scoreNames[3]);
                scoreSettings.Add("score4", GlobalVars._scoreNumbers[3]);

                scoreSettings.Add("name5", GlobalVars._scoreNames[4]);
                scoreSettings.Add("score5", GlobalVars._scoreNumbers[4]);

            }

            #endregion

            #region timer
            try
            {
                appSettings["timerOn"] = GlobalVars._timerOn;

            }
            catch
            {
                appSettings.Add("timerOn", GlobalVars._timerOn);
            }
            #endregion
        }

        // Code to execute if a navigation fails
        private void RootFrame_NavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            if (System.Diagnostics.Debugger.IsAttached)
            {
                // A navigation has failed; break into the debugger
                System.Diagnostics.Debugger.Break();
            }
        }

        // Code to execute on Unhandled Exceptions
        private void Application_UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            if (System.Diagnostics.Debugger.IsAttached)
            {
                // An unhandled exception has occurred; break into the debugger
                System.Diagnostics.Debugger.Break();
            }
        }

        #region Phone application initialization

        // Avoid double-initialization
        private bool phoneApplicationInitialized = false;

        // Do not add any additional code to this method
        private void InitializePhoneApplication()
        {
            if (phoneApplicationInitialized)
                return;

            // Create the frame but don't set it as RootVisual yet; this allows the splash
            // screen to remain active until the application is ready to render.
            RootFrame = new PhoneApplicationFrame();
            RootFrame.Navigated += CompleteInitializePhoneApplication;

            // Handle navigation failures
            RootFrame.NavigationFailed += RootFrame_NavigationFailed;

            // Ensure we don't initialize again
            phoneApplicationInitialized = true;
        }

        // Do not add any additional code to this method
        private void CompleteInitializePhoneApplication(object sender, NavigationEventArgs e)
        {
            // Set the root visual to allow the application to render
            if (RootVisual != RootFrame)
                RootVisual = RootFrame;

            // Remove this handler since it is no longer needed
            RootFrame.Navigated -= CompleteInitializePhoneApplication;
        }

        #endregion

    }
}
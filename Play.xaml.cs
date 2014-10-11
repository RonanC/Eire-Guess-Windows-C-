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
using System.Windows.Threading;
using System.IO.IsolatedStorage;


namespace Eire_Guess
{
    public partial class Play : PhoneApplicationPage
    {
        List<string> countyList = new List<string>{
                                "Antrim",
                                "Armagh",
                                "Carlow",
                                "Cavan",
                                "Clare",
                                "Cork",
                                "Derry",
                                "Donegal",
                                "Down",
                                "Dublin",
                                "Fermanagh",
                                "Galway",
                                "Kerry",
                                "Kildare",
                                "Kilkenny",
                                "Laois",
                                "Leitrim",
                                "Limerick",
                                "Longford",
                                "Louth",
                                "Mayo",
                                "Meath",
                                "Monaghan",
                                "Offaly",
                                "Roscommon",
                                "Sligo",
                                "Tipperary",
                                "Tyrone",
                                "Waterford",
                                "Westmeath",
                                "Wexford",
                                "Wicklow"
                                };

        string[] canvasArray = new string[32];

        List<string> countyRandList = new List<string>();

        Random random = new Random();

        int countyCount = 0;

        // 1000ms = one second

        // A variable to count with.
        const int WAIT = 2;
        const int GUESS_TIME = 5+1;
        const int OVER_TIME = 3;
        const int POINTS = 10;

        double waitCount = WAIT;
        int guessCount = GUESS_TIME;
        int overCount = OVER_TIME;
        //int guessWaitCount = WAIT;

        // waiting bool stops double tap 
        static bool waiting;

        DispatcherTimer waitTimer = new DispatcherTimer();
        DispatcherTimer guessTimer = new DispatcherTimer();
        DispatcherTimer overTimer = new DispatcherTimer();
        //DispatcherTimer guessWaitTimer = new DispatcherTimer();

        # region msdnTimer

        // Raised every 100 miliseconds while the DispatcherTimer is active.

        public void Wait_Tick(object o, EventArgs sender)
        {
            //waiting = true;
            waitCount--;

            

            //TimerBlock.Text = waitCount.ToString();

            if (waitCount <= 0)
            {
                waitCount = WAIT;
                //TimerBlock.Text = waitCount.ToString();

                foreach (Canvas child in CountyHotspot.Children)
                {
                    if ((String)child.Tag == countyRandList[countyCount])
                    {
                        child.Opacity = 0;
                    }
                }

                countyCount++;

                if (countyCount < 32)
                {
                    QuestionBlock.Text = countyRandList[countyCount];
                }

                waitTimer.Stop();

                transImage.Visibility = Visibility.Collapsed;
                waiting = false;
                if (GlobalVars._timerOn == true)
                {
                    guessCount = GUESS_TIME;
                    TimerBlock.Text = "5";
                }

                gameOver();

                if (GlobalVars._timerOn == true)
                {
                    TimerBlock.Visibility = Visibility.Visible;
                }
            }
        }


        // guess tick is only a second long, so a tick within a tick doesn't work
        public void Guess_Tick(object o, EventArgs sender)
        {
            guessCount--;

            if ((guessCount - 1) > 0)
            {
                TimerBlock.Text = (guessCount - 1).ToString();
            }

            if (guessCount == 1)
            {
                transImage.Visibility = Visibility.Visible;
                //waiting = true;
                TimerBlock.Text = (GUESS_TIME - 1).ToString();
                TimerBlock.Visibility = Visibility.Collapsed;

                QuestionBlock.Text = "Too Slow";
                foreach (Canvas child in CountyHotspot.Children)
                {
                    if ((String)child.Tag == countyRandList[countyCount])
                    {
                        child.Opacity = 1;
                    }
                }
            }

            if (guessCount == 0)
            {
                transImage.Visibility = Visibility.Collapsed;
                //waiting = false;
                guessCount = GUESS_TIME;
                TimerBlock.Text = (guessCount-1).ToString();
                TimerBlock.Visibility = Visibility.Visible;

                foreach (Canvas child in CountyHotspot.Children)
                {
                    if ((String)child.Tag == countyRandList[countyCount])
                    {
                        child.Opacity = 0;
                    }
                }

                countyCount++;

                if (countyCount < 32)
                {
                    QuestionBlock.Text = countyRandList[countyCount];
                }

                guessTimer.Stop();
            }

            if (countyCount < 32)
            {
                guessTimer.Start();
            }

            gameOver();
        }

        public void Over_Tick(object o, EventArgs sender)
        {
            overCount--;

            if (overCount == 0)
            {
                overCount = OVER_TIME;
                overTimer.Stop();
                NavigationService.Navigate(new Uri("/Scores.xaml", UriKind.RelativeOrAbsolute));
            }
        }

        //public void GuessWait_Tick(object o, EventArgs sender)
        //{
        //    guessWaitCount--;

        //    if (guessWaitCount == 0)
        //    {
        //        guessWaitCount = 5;

        //        foreach (Canvas child in CountyHotspot.Children)
        //        {
        //            if ((String)child.Tag == countyRandList[countyCount])
        //            {
        //                child.Opacity = 0;
        //            }
        //        }

        //        guessWaitTimer.Stop();
        //    }
        //}

        #endregion 
        public Play()
        {
            InitializeComponent();

            waitTimer.Interval = new TimeSpan(0, 0, 0, 0, 500); // 1/2 second
            waitTimer.Tick += new EventHandler(Wait_Tick);

            guessTimer.Interval = new TimeSpan(0, 0, 0, 1, 0); // 1 second
            guessTimer.Tick += new EventHandler(Guess_Tick);

            overTimer.Interval = new TimeSpan(0,0,1);
            overTimer.Tick += new EventHandler(Over_Tick);

            //guessWaitTimer.Interval = new TimeSpan(0,0,1);
            //guessWaitTimer.Tick += new EventHandler(GuessWait_Tick);
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            /*foreach(string i in countyList)
            {
                QuestionBlock.Text += i.ToString();
            }

            int tempRand = ;*/

            //RandomOrder(countyList);

            Random random = new Random();

            while(countyList.Count != 0)
            {
                int randNum = random.Next(0, countyList.Count);

                countyRandList.Add(countyList[randNum]);
                countyList.RemoveAt(randNum);
            }

            /*foreach(string i in countyList)
            {
                QuestionBlock.Text += i.ToString();
            }*/


            QuestionBlock.Text = countyRandList[0];

            //QuestionBlock.Text = countyRandList[0].ToString();

            //transImage.Visibility = Visibility.Collapsed;
            waiting = false;
            if (GlobalVars._timerOn == true)
            {
                guessTimer.Start();
            }

            if (GlobalVars._timerOn == false)
            {
                TimerBlock.Visibility = Visibility.Collapsed;
            }
        }

        private void Guess_Tap(object sender, GestureEventArgs e)
        {
            TimerBlock.Visibility = Visibility.Collapsed;
            foreach (Canvas child in CountyHotspot.Children)
            {

                if ((String)child.Tag == countyRandList[countyCount])
                {
                    child.Opacity = 1;
                }
            }

            if (GlobalVars._timerOn == true)
            {
                guessTimer.Stop();
            }

            Canvas current = (Canvas)sender;
            
            if (waiting == false)
            {
                transImage.Visibility = Visibility.Visible;
                waiting = true;

                if(countyCount < 32)
                {

                    string countyGuess = current.Tag.ToString();

                    #region correctWrong
                    if (countyGuess == countyRandList[countyCount])
                    {
                        // update score by 100
                        int tempInt;
                        tempInt = Int32.Parse(ScoreBlock.Text);
                        tempInt += POINTS;
                        string tempStr;
                        tempStr = tempInt.ToString();
                        ScoreBlock.Text = tempStr;

                        QuestionBlock.Text = "Correct";
                        
                        //guessWait.Start();
                        waitTimer.Start();
                    }
                    else
                    {
                        QuestionBlock.Text = "Wrong";
                        //guessWait.Start();
                        waitTimer.Start();
                    } // end == if
                    #endregion
                } // end county < 32 if

                if(GlobalVars._timerOn == true)
                {
                    guessTimer.Start();
                }

            } // end bool waiting if


        }

        //private void okButton_Click(object sender, RoutedEventArgs e)
        //{
        //    NavigationService.Navigate(new Uri("/Scores.xaml", UriKind.RelativeOrAbsolute));
        //}

        //private void countyHighlight()
        //{
        //    foreach (Canvas child in CountyHotspot.Children)
        //    {
        //        //canvasArray[iCan] = child.Name;
        //        //iCan++;

        //        if (child.Name == countyRandList[countyCount])
        //        {
        //            child.Opacity = 1;
        //        }
        //    }
        //}

        private void gameOver()
        {
            if (countyCount == 32)
            {
                transImage.Visibility = Visibility.Visible;
                waiting = true;

                TimerBlock.Opacity = 0;
                QuestionBlock.Text = "Game over";
                
                
                if (GlobalVars._timerOn == true)
                {
                    guessTimer.Stop();
                }

                #region sorting scores

                //for (int iScore = 0; iScore < GlobalVars._scoreNames.Length; iScore++ )

                //if (GlobalVars._scoreNumbers == null)
                //{
                //    foreach (int i in GlobalVars._scoreNumbers)
                //    {
                //        GlobalVars._scoreNumbers[i] = 0;
                //        GlobalVars._scoreNames[i] = " ";
                //    }
                //}

                if (Int32.Parse(ScoreBlock.Text) > GlobalVars._scoreNumbers[0])  // 1
                {
                    GlobalVars._scoreNumbers[4] = GlobalVars._scoreNumbers[3];
                    GlobalVars._scoreNames[4] = GlobalVars._scoreNames[3];

                    GlobalVars._scoreNumbers[3] = GlobalVars._scoreNumbers[2];
                    GlobalVars._scoreNames[3] = GlobalVars._scoreNames[2];

                    GlobalVars._scoreNumbers[2] = GlobalVars._scoreNumbers[1];
                    GlobalVars._scoreNames[2] = GlobalVars._scoreNames[1];

                    GlobalVars._scoreNumbers[1] = GlobalVars._scoreNumbers[0];
                    GlobalVars._scoreNames[1] = GlobalVars._scoreNames[0];

                    GlobalVars._scoreNumbers[0] = Int32.Parse(ScoreBlock.Text);
                    GlobalVars._scoreNames[0] = GlobalVars._userName;
                }
                else if (Int32.Parse(ScoreBlock.Text) > GlobalVars._scoreNumbers[1]) // 2
                {
                    GlobalVars._scoreNumbers[4] = GlobalVars._scoreNumbers[3];
                    GlobalVars._scoreNames[4] = GlobalVars._scoreNames[3];

                    GlobalVars._scoreNumbers[3] = GlobalVars._scoreNumbers[2];
                    GlobalVars._scoreNames[3] = GlobalVars._scoreNames[2];

                    GlobalVars._scoreNumbers[2] = GlobalVars._scoreNumbers[1];
                    GlobalVars._scoreNames[2] = GlobalVars._scoreNames[1];

                    GlobalVars._scoreNumbers[1] = Int32.Parse(ScoreBlock.Text);
                    GlobalVars._scoreNames[1] = GlobalVars._userName;
                }
                else if (Int32.Parse(ScoreBlock.Text) > GlobalVars._scoreNumbers[2]) // 3
                {
                    GlobalVars._scoreNumbers[4] = GlobalVars._scoreNumbers[3];
                    GlobalVars._scoreNames[4] = GlobalVars._scoreNames[3];

                    GlobalVars._scoreNumbers[3] = GlobalVars._scoreNumbers[2];
                    GlobalVars._scoreNames[3] = GlobalVars._scoreNames[2];

                    GlobalVars._scoreNumbers[2] = Int32.Parse(ScoreBlock.Text);
                    GlobalVars._scoreNames[2] = GlobalVars._userName;
                }
                else if (Int32.Parse(ScoreBlock.Text) > GlobalVars._scoreNumbers[3]) // 4
                {
                    GlobalVars._scoreNumbers[4] = GlobalVars._scoreNumbers[3];
                    GlobalVars._scoreNames[4] = GlobalVars._scoreNames[3];

                    GlobalVars._scoreNumbers[3] = Int32.Parse(ScoreBlock.Text);
                    GlobalVars._scoreNames[3] = GlobalVars._userName;
                }
                else if (Int32.Parse(ScoreBlock.Text) > GlobalVars._scoreNumbers[4]) // 5
                {
                    GlobalVars._scoreNumbers[4] = Int32.Parse(ScoreBlock.Text);
                    GlobalVars._scoreNames[4] = GlobalVars._userName;
                }

                #endregion

                overTimer.Start();
                
            }
        }

        //private void guessWaiter()
        //{

        //    guessWaitTimer.Start();

        //}
    }
}
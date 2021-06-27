using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace iq_project
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window // סידור לוח המשחק 
    {
        public int limitLabel = 250;
        public int spaceBetweenGumot = 50;
        public int spaceRelativelyBoard_X = 55;
        public int spaceRelativelyBoard_Y = 50;

        public Board b = new Board();

        public CardBoard c = new CardBoard();

        DispatcherTimer timer;
        public TimeSpan time;
        public TimeSpan startTime;//כדי לדעת כמה זמן לקח לפתור שלב
        public TimeSpan endTime;//כדי לדעת כמה זמן לקח לפתור שלב
        public int allTimeInSeconds;
        TimeSpan timeToHint = TimeSpan.FromSeconds(9999999);//כדי לדעת מתי להחזיר צורה לצבע המקורי אחרי מתן רמז

        public int score { get; internal set; }

        public MainWindow()
        {

            InitializeComponent();
            levelLabel.Content = "   level\n      " + ClsGlobal.numOfCard.ToString();
            allTimeInSeconds = (ClsGlobal.NumLevel + 1) * 100;
            time = TimeSpan.FromSeconds(allTimeInSeconds);
            startTime = time;
            timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
            {
                if (time == TimeSpan.Zero)
                {
                    timer.Stop();
                    End();
                }
                if (time == timeToHint)///ברגע שעברו 3 שניות מהרמז
                {
                    b.backToBlack();
                    //st.backToBlack();
                }
                labeltimer.Content = time.ToString();
                time = time.Add(TimeSpan.FromSeconds(-1));
            }, Application.Current.Dispatcher);

            timer.Start();



            //בניית הצורות שלא ניתנות להזזה על פי דפי ה-xml 
            Grid.SetRow(b, 0);
            Grid.SetColumn(b, 0);
            g.Children.Add(b);

            // בניית הצורות הנותרות- שלא נמצאות בלוח         
            Grid.SetRow(c, 1);
            Grid.SetColumn(c, 1);
            g.Children.Add(c);
        }

        public void End()
        {
            ClsGlobal.Level.onPlay.Visibility = Visibility.Hidden;
            for (int i = 0; i < 10; i++)
            {
                ClsGlobal.shapesUse[i] = 0;
            }
            Close();
        }

        private void Button_Click_X(object sender, RoutedEventArgs e)
        {
            End();
        }

        private void Button_Click_Hint(object sender, RoutedEventArgs e)
        {
            b.Hint();
            timeToHint = time;//שומרים את הזמן שבו הרמז מתחיל
            timeToHint = timeToHint.Add(TimeSpan.FromSeconds(-3));//ומורידים ממנו 3 שניות(מתי שהשעון שבמשחק ישתווה אליו נחזיר את מסגרת הגומות לצבע המקורי

        }
    }
}

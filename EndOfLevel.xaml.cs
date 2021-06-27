using System.Windows;
using System.Windows.Controls;

namespace iq_project
{
    /// <summary>
    /// Interaction logic for EndOfLevel.xaml
    /// </summary>
    public partial class EndOfLevel : Window
    {
        public EndOfLevel()
        {
            InitializeComponent();
            ClsGlobal.Level.onWinn.Visibility = Visibility.Visible;
        }

        public EndOfLevel(int secondsTextBox, int minutesTextBox, int scoreTextBox, int sumScoreTextBox, int stepsTextBox)
        {
            this.secondsTextBox = new TextBox();
            this.minutesTextBox = new TextBox();
            this.scoreTextBox = new TextBox();
            this.sumScoreTextBox = new TextBox();
            this.stepsTextBox = new TextBox();
            this.secondsTextBox.Text = secondsTextBox.ToString();
            this.minutesTextBox.Text = minutesTextBox.ToString();
            this.scoreTextBox.Text = scoreTextBox.ToString();
            this.sumScoreTextBox.Text = (sumScoreTextBox + scoreTextBox).ToString();
            this.stepsTextBox.Text = stepsTextBox.ToString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < 10; i++)//איפוס מערך העזר של שמירת הצורות
            {
                ClsGlobal.shapesUse[i] = 0;
            }
            ClsGlobal.numOfCard += 1;
            ClsGlobal.Set_Level(ClsGlobal.Level.Next_Level);
            ClsGlobal.Level.onPlay.Visibility = Visibility.Visible;//צביעת לחצן השלב בצבע ירוק
            MainWindow mw = new MainWindow();//פתיחת שלב חדש
            this.Close();//סגיר השלב הנוכחי
            mw.Show();
        }
    }
}

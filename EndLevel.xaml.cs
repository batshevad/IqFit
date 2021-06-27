using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace iq_project
{
    /// <summary>
    /// Interaction logic for EndLevel.xaml
    /// </summary>
    public partial class EndLevel : Window
    {
        public EndLevel()
        {
            InitializeComponent();
            //ClsGlobal.Level.onWinn.Visibility = Visibility.Visible;
        }

        public EndLevel(int seconds, int minutes, int allTime, int steps)
        {
            InitializeComponent();
            ClsGlobal.Level.onWinn.Visibility = Visibility.Visible;//סימון השלב בצבע ירוק 
            int sourceIndex = (int)ClsGlobal.Level.Num_level.Content;
            System.Text.StringBuilder strBuilder = new System.Text.StringBuilder(ClsGlobal.DoneLevels);
            strBuilder[sourceIndex - 1] = '1';
            ClsGlobal.DoneLevels = strBuilder.ToString();//שינוי המחרוזת ששומרת את השלבים שנעשו
            //לבדוק שזה עובד כמו שצריך, בעז"V!!!
            int levelScore = Score(steps, (seconds + (minutes * 60)) / allTime);//חישוב הנקודות

            secondsTextBox.Content = seconds.ToString();
            minutesTextBox.Content = minutes.ToString();
            scoreTextBox.Content = levelScore.ToString();
            sumScoreTextBox.Content = (ClsGlobal.sumScore + levelScore).ToString();
            stepsTextBox.Content = steps.ToString();
            ClsGlobal.sumScore += levelScore;

            try
            {
                Select("UPDATE [dbo].[Users] SET sumScore='" + ClsGlobal.sumScore + "', doneLevels='" + ClsGlobal.DoneLevels + "'  WHERE [userName]= '" + ClsGlobal.userName + "'");//שמירת פרטי המשחק, כדי שבפעם הבאה אפשר יהיה להמשיך מאיפה שנעצרו

            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }


        }
        //פונקציה לחישוב ניקוד: כמה שפחות צעדים ופחות זמן (יחסית לזמן שניתן) ככה הניקוד יותר גבוה
        //מה שעושים- מכפילים את מס' הצעדים ביחס שבין הזמן שלקח לפתור לזמן שניתן מלכתכילא, ומחסירים את התוצאה מ1200 (רמז ל-120 שלבי במשחק)
        private int Score(int steps, int seconds)
        {
            return 1200 - steps * seconds;//זה לא עובד, בעז"ה לבדוק- שאלה
        }

        public DataTable Select(string selectSQL)
        {
            DataTable dataTable = new DataTable("dataBase");                                                                                         // подключаемся к базе данных
            SqlConnection sqlConnection = new SqlConnection(@"Data Source =(LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\Users\Avraham\AppData\Local\Microsoft\Microsoft SQL Server Local DB\Instances\MSSQLLocalDB\New Database.mdf ");
            sqlConnection.Open();
            SqlCommand sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandText = selectSQL;
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            sqlDataAdapter.Fill(dataTable);          
            return dataTable;


        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {//את כל ההתחלה צריך להוצא מהפונקציה, כי זה נעשה בכל מקרה
            for (int i = 0; i < 10; i++)//איפוס מערך העזר של שמירת הצורות
            {
                ClsGlobal.shapesUse[i] = 0;
            }
            ClsGlobal.numOfCard += 1;
            ClsGlobal.Set_Level(ClsGlobal.Level.Next_Level);
            ClsGlobal.Level.onPlay.Visibility = Visibility.Visible;//צביעת לחצן השלב בצבע אדום
            MainWindow mw = new MainWindow();//פתיחת שלב חדש
            this.Close();//סגיר השלב הנוכחי
            mw.Show();
        }

        private void records_button_Click(object sender, RoutedEventArgs e)
        {
            recordsTable table = new recordsTable();
            this.Close();
            table.Show();
        }
    }

}
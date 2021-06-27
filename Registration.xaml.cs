using System.Windows;
using System.Windows.Controls;
using System.Data.SqlClient;
using System.Data;
using System;
using MessageBox = System.Windows.MessageBox;

namespace iq_project
{
    /// <summary>
    /// Interaction logic for Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        public Registration()
        {
            InitializeComponent();
        }
        public DataTable Select(string selectSQL) // חיבור למסד הנתונים 
        {
            DataTable dataTable = new DataTable("dataBase");                // יצירת טבלה                                                                            
            SqlConnection sqlConnection = new SqlConnection(@"Data Source =(LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\Users\Avraham\AppData\Local\Microsoft\Microsoft SQL Server Local DB\Instances\MSSQLLocalDB\New Database.mdf ");// חיבור למסד הנתונים
            sqlConnection.Open();                                           // פתיחת מסד הנתונים
            SqlCommand sqlCommand = sqlConnection.CreateCommand();          // הפקודה
            sqlCommand.CommandText = selectSQL;                             // הפיכה לטקסט
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand); 
            sqlDataAdapter.Fill(dataTable);                                 // החזרת הטבלה עם הפקודה שנשלחה
            return dataTable;
        }

       // string dbConnector = @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\Users\israe\Dropbox\פרויקט איי קיו\DB\iqGameDB.mdf; Integrated Security = True; Connect Timeout = 30 ";

        private void NewGameButton_Click(object sender, RoutedEventArgs e)
        {
            newNameLabel.Visibility = Visibility.Visible;
            newNameTextBox.Visibility = Visibility.Visible;
        }

        private void nameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            saveAndPlayButton.Visibility = Visibility.Visible;
        }

        private void SavedGameButton_Click(object sender, RoutedEventArgs e)
        {
            newNameLabel.Visibility = Visibility.Visible;
            savedNameTextBox.Visibility = Visibility.Visible;
        }

        private void savedNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            PlayButton.Visibility = Visibility.Visible;
        }

        private void SavedAndPlayButton_Click(object sender, RoutedEventArgs e)
        {
            string doneLevel = "0";//פה מוסיפים משתנה שיעזור לדעת איזה שלבים השחקן כבר עשה, בשביל שמירת המשחק
            for(int i=1; i<120; i++)          
                doneLevel += "0";//בהתחלה זה 120 אפסים, כאשר עוברים שלב האפס באינדקס של השלב הופך ל-1
            ClsGlobal.DoneLevels = doneLevel;
            try
            {
                Select("INSERT INTO [dbo].[Users]  VALUES ('" + newNameTextBox.Text + "','0','"+doneLevel+"')");//עדכון משתנים של שחקן חדש
                ClsGlobal.userName = newNameTextBox.Text;
                ClsGlobal.userNameString = "hello " + newNameTextBox.Text;
                LevelPage l = new LevelPage();
                Close();
                l.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("name like this exists in sistem");
            }
        }

        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {


            // חיפוש משתמש עם שם כזה
            DataTable dt_user = Select("SELECT * FROM [dbo].[Users] WHERE [userName] = '" + savedNameTextBox.Text +"'");
            if (dt_user.Rows.Count > 0) // אם הוא קיים
            {
                MessageBox.Show("welcome back!"); // יוצאת הודעה מתאימה
                ClsGlobal.userNameString = "welcome back " + savedNameTextBox.Text;
                ClsGlobal.userName= savedNameTextBox.Text;
                DataTable dt = new DataTable();
                dt= Select("SELECT [sumScore] FROM [dbo].[Users] WHERE [userName] = '" + savedNameTextBox.Text + "'");//שולפים את הפרטים של השחקן
                ClsGlobal.sumScore= dt.Rows[0].Field<int>(0);//ושומרי םכמספר
                dt = Select("SELECT [doneLevels] FROM [dbo].[Users] WHERE [userName] = '" + savedNameTextBox.Text + "'");//שולפים את הפרטים של השחקן
                ClsGlobal.DoneLevels=dt.Rows[0].Field<string>(0);//ושומרים כמחרוזת
                LevelPage l = new LevelPage();
                Close();          
                l.ShowDialog();
            }
            else notCorrectNameLabel.Visibility = Visibility.Visible;
        }
    }
}

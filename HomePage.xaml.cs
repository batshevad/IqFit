using System.Windows;
using System.Windows.Controls;

namespace iq_project
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Window
    {

        public HomePage()
        {
            InitializeComponent();
            nameLabel.Content = ClsGlobal.userNameString;
            scoreLabel.Content = "your score is " + ClsGlobal.sumScore.ToString();
            num_Of_Level(ClsGlobal.NumLevel);
        }
        int[] rowSet = { 0, 1, 0, 1, 2, 1, 2, 2, 1, 0 };//יעזור לסדר בלוח את הלחצנים
        int[] columnSet = {0,1,2,3,4,5,6,7,8,9 };//יעזור לסדר בלוח את הלחצנים

        void num_Of_Level(int Cls)//פונקציה שכותבת את מספרי השלבים על הלחצנים 
        {
            NewLevel[] levels = new NewLevel[10];
            levels[0]= new NewLevel();
            for (int i = 0; i < 10; i++)
            {
                levels[i].Num_level.Content = Cls * 10 + i + 1;//מספר השלב נכתב על הלחצן
                Grid.SetRow(levels[i], rowSet[i]);
                Grid.SetColumn(levels[i], columnSet[i]);
                g.Children.Add(levels[i]);
                if (i < 9)
                {
                    levels[i + 1] = new NewLevel();
                    levels[i].Set_Level(levels[i + 1]);
                }
                //if (ClsGlobal.DoneLevels[Cls * 10 + i] == '1')//זה בא לסמן את השלבים שהשחקן עשה במשחקים קודמים
                //{
                //    levels[i].onWinn.Visibility = Visibility.Visible;
                //}
            }
        }

       
        private void Button_Click_1(object sender, RoutedEventArgs e)//כאשר לוחצים על -> הלוח "זז" 10 שלבים קדימה
        {
            if ( ClsGlobal.NumLevel != 0)
            {
                 ClsGlobal.NumLevel--;
                HomePage H = new HomePage();
                this.Close();
                H.ShowDialog();
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)//כאשר לוחצים על <- הלוח "זז" 10 שלבים אחורה
        {
            if ( ClsGlobal.NumLevel != 11)
            {
                 ClsGlobal.NumLevel++;
                HomePage H = new HomePage();
                this.Close();
                H.ShowDialog();
            }
        }
    }
}
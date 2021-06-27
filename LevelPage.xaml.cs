using System.Windows;
using System.Windows.Controls;

namespace iq_project
{
    /// <summary>
    /// Interaction logic for LevelPage.xaml
    /// </summary>
    public partial class LevelPage : Window
    {
        public LevelPage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)//הכפתור הזה לא עובד, אם ישאר זמן, זה לא נראה מסובך מאד בעז"ה
        {
            if (LB.SelectedItem != null)
            {
                switch (LB.SelectedItem.ToString())
                {
                    case "System.Windows.Controls.ListBoxItem: 1":
                        ClsGlobal.NumLevel = 0;
                        break;
                    case "System.Windows.Controls.ListBoxItem: 2":
                        ClsGlobal.NumLevel = 1;
                        break;
                    case "System.Windows.Controls.ListBoxItem: 3":
                        ClsGlobal.NumLevel = 2;
                        break;
                    case "System.Windows.Controls.ListBoxItem: 4":
                        ClsGlobal.NumLevel = 3;
                        break;
                    case "System.Windows.Controls.ListBoxItem: 5":
                        ClsGlobal.NumLevel = 4;
                        break;
                    case "System.Windows.Controls.ListBoxItem: 6":
                        ClsGlobal.NumLevel = 5;
                        break;
                    case "System.Windows.Controls.ListBoxItem: 7":
                        ClsGlobal.NumLevel = 6;
                        break;
                    case "System.Windows.Controls.ListBoxItem: 8":
                        ClsGlobal.NumLevel = 7;
                        break;
                    case "System.Windows.Controls.ListBoxItem: 9":
                        ClsGlobal.NumLevel = 8;
                        break;
                    case "System.Windows.Controls.ListBoxItem: 10":
                        ClsGlobal.NumLevel = 9;
                        break;
                    case "System.Windows.Controls.ListBoxItem: 11":
                        ClsGlobal.NumLevel = 10;
                        break;
                    case "System.Windows.Controls.ListBoxItem: 12":
                        ClsGlobal.NumLevel = 11;
                        break;
                }
            }
            HomePage H = new HomePage();
            this.Close();
            H.ShowDialog();
        }


        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LB.SelectedItem != null)
            {
                switch (LB.SelectedItem.ToString())
                {
                    case "System.Windows.Controls.ListBoxItem: 1-10":
                        ClsGlobal.NumLevel = 0;
                        break;
                    case "System.Windows.Controls.ListBoxItem: 10-20":
                        ClsGlobal.NumLevel = 1;
                        break;
                    case "System.Windows.Controls.ListBoxItem: 20-30":
                        ClsGlobal.NumLevel = 2;
                        break;
                    case "System.Windows.Controls.ListBoxItem: 30-40":
                        ClsGlobal.NumLevel = 3;
                        break;
                    case "System.Windows.Controls.ListBoxItem: 40-50":
                        ClsGlobal.NumLevel = 4;
                        break;
                    case "System.Windows.Controls.ListBoxItem: 50-60":
                        ClsGlobal.NumLevel = 5;
                        break;
                    case "System.Windows.Controls.ListBoxItem: 60-70":
                        ClsGlobal.NumLevel = 6;
                        break;
                    case "System.Windows.Controls.ListBoxItem: 70-80":
                        ClsGlobal.NumLevel = 7;
                        break;
                    case "System.Windows.Controls.ListBoxItem: 80-90":
                        ClsGlobal.NumLevel = 8;
                        break;
                    case "System.Windows.Controls.ListBoxItem: 90-100":
                        ClsGlobal.NumLevel = 9;
                        break;
                    case "System.Windows.Controls.ListBoxItem: 100-110":
                        ClsGlobal.NumLevel = 10;
                        break;
                    case "System.Windows.Controls.ListBoxItem: 110-120":
                        ClsGlobal.NumLevel = 11;
                        break;
                    default:
                        break;
                }
                HomePage H = new HomePage();
                this.Close();
                H.ShowDialog();
            }
        }

        private void ListBoxItem_Selected(object sender, RoutedEventArgs e)
        {

        }
    }
}



using System;
using System.Windows;
using System.Windows.Controls;

namespace iq_project
{

    public partial class NewLevel : UserControl
    {


        public NewLevel()// לחצן למעבר ללוח המשחק
        {
            InitializeComponent();
        }

        public NewLevel Next_Level;

        public void Set_Level(NewLevel NL) => Next_Level = NL;

        public void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Button But = sender as Button;
            ClsGlobal.numOfCard = Convert.ToInt32(Num_level.Content);//פתיחה של דף משחק ע"פ מספר השלב המתאים
            ClsGlobal.Set_Level(this);
            MainWindow MW = new MainWindow();//יצירה של דף המשחק
            MW.Show();//פתיחה של דף המשחק
            onPlay.Visibility = Visibility.Visible;//סימון הכפתור, ככפתור שעכשיו משחקים בו
            
        }


        public void Button_on_winn()
        {
            onWinn.Visibility = Visibility.Visible;
        }

    }
}

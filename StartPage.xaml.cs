using System.Windows;




namespace iq_project
{
    /// <summary>
    /// Interaction logic for StartPage.xaml
    /// </summary>
    public partial class StartPage : Window
    {
        public StartPage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)//להוסיף אפקטים? שאלה
        {
            Registration r = new Registration();
            this.Close();
            r.ShowDialog();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Instructions i = new Instructions();
            this.Close();
            i.ShowDialog();
        }
    }
}

using System.Windows;

namespace iq_project
{
    /// <summary>
    /// Interaction logic for Instructions.xaml
    /// </summary>
    public partial class Instructions : Window
    {
        public Instructions()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Registration r = new Registration();
            Close();
            r.ShowDialog();
        }
    }
}

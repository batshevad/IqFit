using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace iq_project
{
    /// <summary>
    /// Interaction logic for HintMessageBox.xaml
    /// </summary>
    public partial class HintMessageBox : Window
    {
        public HintMessageBox(string col, string label1, string label2)
        {
            InitializeComponent();
            HintLabel.Content = label1+col+label2;
            switch (col)
            {
                case "dark green":
                    HMBGrid.Background = Brushes.Green;
                    break;
                case "purple":
                    HMBGrid.Background = Brushes.Purple;// אם זה צבע סגול
                    break;
                case "light azure":
                    HMBGrid.Background = Brushes.SkyBlue;// אם זה צבע תכלת
                    break;
                case "light green":
                    HMBGrid.Background = Brushes.LightGreen;// אם זה צבע ירוק בהיר 
                    break;
                case "azure":
                    HMBGrid.Background = Brushes.Blue;// אם זה צבע כחול
                    break;
                case "blue":
                    HMBGrid.Background = Brushes.DarkBlue;// אם זה צבע כחול כהה
                    break;
                case "yellow":
                    HMBGrid.Background = Brushes.Yellow;// אם זה צבע צהוב
                    break;
                case "pink":
                    HMBGrid.Background = Brushes.Pink; // אם זה צבע ורוד
                    break;
                case "orange":
                    HMBGrid.Background = Brushes.Orange; // אם זה צבע כתום
                    break;
                case "red":
                    HMBGrid.Background = Brushes.Red; // אם זה צבע אדום
                    break;
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

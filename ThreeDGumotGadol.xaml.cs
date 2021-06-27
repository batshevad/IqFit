using System;
using System.Windows.Controls;

namespace iq_project
{
    /// <summary>
    /// Interaction logic for ThreeDGumotGadol.xaml
    /// </summary>
    public partial class ThreeDGumotGadol : UserControl//גומות תלת מימדיות גדולות
    {
        public ThreeDGumotGadol()
        {
            InitializeComponent();
        }
        private void OnWindowLoaded(object sender, EventArgs e)
        {
           
            myViewport3D.Focus();
        }

    }
}

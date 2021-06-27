using System.Windows.Controls;

namespace iq_project
{
    /// <summary>
    /// Interaction logic for Gumot.xaml
    /// </summary>
    public partial class Gumot : UserControl //בניית גומה פשוטה
    {
        
        public Gumot()
        {
            InitializeComponent();
        }
        private bool isTaken = false;
        private bool hintTaken = false;

        public bool IsTaken { get => isTaken; set => isTaken = value; }
        public bool HintTaken { get => hintTaken; set => hintTaken = value; }

    }
}

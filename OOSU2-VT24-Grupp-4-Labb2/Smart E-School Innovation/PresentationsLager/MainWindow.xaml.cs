using System.Windows;

namespace PresentationsLager
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            Alternativen valFönster = new Alternativen();
            valFönster.Show();
            this.Close();
        }
    }
}
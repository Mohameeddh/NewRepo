using System.Windows;

namespace WpfApp222222
{
    public partial class MainWindow : Window
    {
        bool running = false;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnRun_Click(object sender, RoutedEventArgs e)
        {
            tbHello.Text = "Running";
            running = true;
        }
    }
}
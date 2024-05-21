using System.Windows;

namespace PresentationsLager
{
    public partial class Alternativen : Window
    {
        public Alternativen()
        {
            InitializeComponent();
        }

        private void btnGenereraFragor_Click(object sender, RoutedEventArgs e)
        {
            FrågeGenerator frågeGenerator = new FrågeGenerator();
            frågeGenerator.Show();
            this.Close();
        }

        private void btnSammanfattaKapitel_Click(object sender, RoutedEventArgs e)
        {
            Sammanfattning sammanfattning = new Sammanfattning();
            sammanfattning.Show();
            this.Close();
        }
    }
}
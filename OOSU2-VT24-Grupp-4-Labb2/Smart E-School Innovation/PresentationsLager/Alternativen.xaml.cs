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
            // Här kan du lägga till kod för att öppna fönstret eller funktionen för att sammanfatta kapitel
            MessageBox.Show("Sammanfatta kapitel-funktionen är ännu inte implementerad.");
            this.Close();
        }
    }
}
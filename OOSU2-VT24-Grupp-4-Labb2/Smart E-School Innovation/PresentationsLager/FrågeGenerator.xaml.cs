using Microsoft.Extensions.Configuration;
using OpenAI_API;
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

namespace PresentationsLager
{
    public partial class FrågeGenerator : Window
    {
        private readonly OpenAIAPI openAI;

        public FrågeGenerator()
        {
            InitializeComponent();

            var configuration = new ConfigurationBuilder().AddUserSecrets<FrågeGenerator>().Build();
            var key = configuration["OpenAI_Key"];

            if (string.IsNullOrEmpty(key))
            {
                MessageBox.Show("OpenAI nyckel saknas");
                Close();
            }

            // Skapa en OpenAI-klient med den tillhandahållna nyckeln
            openAI = new OpenAIAPI(key);
        }

        private async void GenereraFrågor_Click(object sender, RoutedEventArgs e)
        {
            string selectedKurslitteratur = txtKurslitteratur.Text;
            string selectedÅrskurs = txtÅrskurs.Text;
            int ålder = int.Parse(txtÅlder.Text);

            // Skapa prompt för att generera frågor
            string prompt = $"{selectedKurslitteratur} {selectedÅrskurs} Include questions related to..."; // Anpassa prompten efter ditt användningsområde

            // Generera frågor med OpenAI API
            var questionResult = await openAI.Completions.CreateCompletionAsync(
                prompt: prompt,
                max_tokens: 100, // Max antal tokens för genereringen
                temperature: 0.5 // Temperaturparameter för att styra variationen i genereringen
            );

            txtGeneratedQuestions.Text = questionResult.ToString();
        }
    }
}

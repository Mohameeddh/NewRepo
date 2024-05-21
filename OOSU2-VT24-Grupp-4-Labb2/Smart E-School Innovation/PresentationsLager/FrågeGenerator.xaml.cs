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

            
            openAI = new OpenAIAPI(key);
        }

        private async void GenereraFrågor_Click(object sender, RoutedEventArgs e)
        {
            string selectedKurslitteratur = txtKurslitteratur.Text;
            string selectedÅrskurs = txtÅrskurs.Text;
            int ålder = int.Parse(txtÅlder.Text);


            string prompt;
            if (selectedKurslitteratur.Contains("matematik", StringComparison.OrdinalIgnoreCase))
            {
                if (selectedKurslitteratur.Contains("1a") || selectedKurslitteratur.Contains("1b") || selectedKurslitteratur.Contains("1c") ||
                    selectedKurslitteratur.Contains("2b") || selectedKurslitteratur.Contains("2c"))
                {
                    prompt = $"Generera matematikfrågor för {selectedKurslitteratur} {selectedÅrskurs}. Fokusera på ämnen som andragradsekvationer och linjära ekvationer.";
                }
                else if (selectedKurslitteratur.Contains("3b") || selectedKurslitteratur.Contains("3c") || selectedKurslitteratur.Contains("4") ||
                         selectedKurslitteratur.Contains("5"))
                {
                    prompt = $"Generera avancerade matematikfrågor för {selectedKurslitteratur} {selectedÅrskurs}. Inkludera ämnen som derivator, integraler och avancerade ekvationer.";
                }
                else
                {
                    prompt = $"Generera grundläggande matematikfrågor för {selectedKurslitteratur} {selectedÅrskurs}.";
                }
            }
            else if (selectedKurslitteratur.Contains("fysik 1", StringComparison.OrdinalIgnoreCase))
            {
                prompt = $"Generera matematikfrågor för {selectedKurslitteratur} {selectedÅrskurs}. Inkludera ämnen som kraft, elektricitet, effekt och vektorer.";
            }

            else if (selectedKurslitteratur.Contains("fysik 2", StringComparison.OrdinalIgnoreCase))
            {
                prompt = $"Generera matematikfrågor för {selectedKurslitteratur} {selectedÅrskurs}. Inkludera ämnen som kraft, elektricitet, effekt och vektorer.";
            }
            else if (selectedKurslitteratur.Contains("kemi 1", StringComparison.OrdinalIgnoreCase))
            {
                prompt = $"Generera matematikfrågor för {selectedKurslitteratur} {selectedÅrskurs}. Fokusera på ämnen som molberäkning och kemiska formler.";
            }
            else if (selectedKurslitteratur.Contains("kemi 2", StringComparison.OrdinalIgnoreCase))
            {
                prompt = $"Generera matematikfrågor för {selectedKurslitteratur} {selectedÅrskurs}. Fokusera på ämnen som molberäkning och kemiska formler.";
            }
            else
            {
                prompt = $"{selectedKurslitteratur} {selectedÅrskurs}. Generera frågor som är relevanta för kursmaterialet.";
            }

            var questionResult = await openAI.Completions.CreateCompletionAsync(
                prompt: prompt,
                max_tokens: 300,
                temperature: 0.5
            );

            txtGeneratedQuestions.Text = questionResult.ToString();
        }
    }
}

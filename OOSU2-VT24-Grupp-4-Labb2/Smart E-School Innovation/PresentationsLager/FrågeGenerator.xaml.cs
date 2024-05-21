using Microsoft.Extensions.Configuration;
using OpenAI_API;
using System;
using System.Windows;

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

        private async void GenereraSvar_Click(object sender, RoutedEventArgs e)
        {
            string questions = txtGeneratedQuestions.Text;

            if (string.IsNullOrWhiteSpace(questions))
            {
                MessageBox.Show("Inga genererade frågor att svara på.");
                return;
            }

            string prompt = $"Här är några frågor: {questions}. Svara på dessa frågor.";

            var answerResult = await openAI.Completions.CreateCompletionAsync(
                prompt: prompt,
                max_tokens: 300,
                temperature: 0.5
            );

            txtGeneratedAnswers.Text = answerResult.ToString();
        }
    }
}
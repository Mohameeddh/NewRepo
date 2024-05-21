using Microsoft.Extensions.Configuration;
using OpenAI_API;
using System;
using System.Windows;

namespace PresentationsLager
{
    
    public partial class Sammanfattning : Window
    {
        private readonly OpenAIAPI openAI;

        public Sammanfattning()
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

        private async void GenereraSammanfattning_Click(object sender, RoutedEventArgs e)
        {
            string selectedKurslitteratur = txtKurslitteratur.Text;
            string kapitel = txtKapitel.Text;
            string selectedÅrskurs = txtÅrskurs.Text;
            int ålder = int.Parse(txtÅlder.Text);

            string prompt = $"Generera en sammanfattning för kapiteln {kapitel} {selectedÅrskurs}. " +
                $"Sammanfatta de viktigaste koncepten och begreppen som täcks i kapiteln. Inludera även sammanfattning och betydelse för olika begrepp.";

            var summaryResult = await openAI.Completions.CreateCompletionAsync(
                prompt: prompt,
                max_tokens: 400,
                temperature: 0.5
            );

            txtGenereradSammanfattning.Text = summaryResult.ToString();
        }
    }
}

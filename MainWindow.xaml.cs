using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Microsoft.Speech.Recognition;
using Microsoft.Speech.Synthesis;

namespace Registration_form
{
    public partial class MainWindow : Window
    {
        private SpeechRecognitionEngine speechRecognitionEngine;
        private SpeechSynthesizer speechSynthesizer;
        private bool isSpeechRecognitionEnabled = false;
        string fullPath = AppDomain.CurrentDomain.BaseDirectory;
        public MainWindow()
        {
            InitializeComponent();
            InitializeSpeechRecognition();
            InitializeSpeechSynthesizer();
        }

        private void InitializeSpeechRecognition()
        {
            CultureInfo cultureInfo = new CultureInfo("pl-PL"); // np. "en-US" dla angielskiego, "pl-PL" dla polskiego
            speechRecognitionEngine = new SpeechRecognitionEngine(new System.Globalization.CultureInfo("pl-PL"));



            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string grammarPath = Path.Combine(basePath, "pl-PL.grxml");
            Grammar grammar = LoadSrgsGrammar(grammarPath);


            speechRecognitionEngine.LoadGrammar(grammar);
            speechRecognitionEngine.SetInputToDefaultAudioDevice();
            speechRecognitionEngine.SpeechRecognized += SpeechRecognized;
        }

        private void InitializeSpeechSynthesizer()
        {
            speechSynthesizer = new SpeechSynthesizer();
            speechSynthesizer.SetOutputToDefaultAudioDevice();
        }



        private Grammar LoadSrgsGrammar(string filePath)
        {
            try
            {
                // Tworzenie gramatyki z pliku SRGS
                Grammar grammar = new Grammar(filePath);

                return grammar;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Wystąpił błąd podczas ładowania gramatyki: {ex.Message}");
                return null;
            }
        }

        private void SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            string recognizedText = e.Result.Text;

            if (Keyboard.FocusedElement is TextBox textBox && recognizedText != "Wyślij formularz")
            {
                textBox.SelectedText += recognizedText;
            }

            if (recognizedText == "Wyślij formularz")
            {
                SaveButton_Click(this, new RoutedEventArgs());
            }

        }

        private void SpeechRecognitionToggle_Checked(object sender, RoutedEventArgs e)
        {
            if (!isSpeechRecognitionEnabled)
            {
                speechRecognitionEngine.RecognizeAsync(RecognizeMode.Multiple);
                speechSynthesizer.Speak("Rozpoczęto uzupełnianie głosowe");
                isSpeechRecognitionEnabled = true;
            }
        }

        private void SpeechRecognitionToggle_Unchecked(object sender, RoutedEventArgs e)
        {
            if (isSpeechRecognitionEnabled)
            {
                speechRecognitionEngine.RecognizeAsyncCancel();
                speechSynthesizer.Speak("Zakończono uzupełnianie głosowe");
                isSpeechRecognitionEnabled = false;
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            ClearBackgroundProperty();
            List<string> unfilledFields = GetUnfilledFields();

            if (unfilledFields.Count == 0)
            {
                ShowMessageBoxWithFormData();
            }
            else
            {
                string message;
                if (unfilledFields.Count == 1)
                {
                    message = $"Uzupełnij pole: {unfilledFields[0]}";
                }
                else
                {
                    message = $"Uzupełnij pola: {string.Join(", ", unfilledFields)}";
                }

                speechSynthesizer.SpeakAsync(message);

                foreach (var field in unfilledFields)
                {
                    HighlightField(field);
                }
            }
        }

        private void ShowMessageBoxWithFormData()
        {
            string message = $"Imię: {FirstNameTextBox.Text}\n"
                           + $"Nazwisko: {LastNameTextBox.Text}\n"
                           + $"Czas trwania podróży: {TravelTimeTextBox.Text}\n"
                           + $"Kraj podróży: {CountryComboBoxTextBox.Text}\n"
                           + $"Preferencje podróżnicze: {TravelPreferenceTextBox.Text}\n"
                           + "Jeżli chcesz poprawić formularz wciśnij okej, kliknij przycisk WYCZYŚĆ i wypełnij ponownie.";
            ClearBackgroundProperty();
            speechSynthesizer.SpeakAsync(message);
            MessageBox.Show(message, "Formularz zapisany!", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private List<string> GetUnfilledFields()
        {
            List<string> unfilledFields = new List<string>();

            if (string.IsNullOrWhiteSpace(FirstNameTextBox.Text))
                unfilledFields.Add("Imię");

            if (string.IsNullOrWhiteSpace(LastNameTextBox.Text))
                unfilledFields.Add("Nazwisko");

            if (string.IsNullOrWhiteSpace(TravelTimeTextBox.Text))
                unfilledFields.Add("Czas trwania podróży");

            if (string.IsNullOrWhiteSpace(CountryComboBoxTextBox.Text))
                unfilledFields.Add("Kraj podróży");

            if (string.IsNullOrWhiteSpace(TravelPreferenceTextBox.Text))
                unfilledFields.Add("Preferencje podróżnicze");

            return unfilledFields;
        }

        private void HighlightField(string fieldName)
        {
            switch (fieldName)
            {
                case "Imię":
                    FirstNameTextBox.Background = Brushes.Yellow;
                    break;
                case "Nazwisko":
                    LastNameTextBox.Background = Brushes.Yellow;
                    break;
                case "Czas trwania podróży":
                    TravelTimeTextBox.Background = Brushes.Yellow;
                    break;
                case "Kraj podróży":
                    CountryComboBoxTextBox.Background = Brushes.Yellow;
                    break;
                case "Preferencje podróżnicze":
                    TravelPreferenceTextBox.Background = Brushes.Yellow;
                    break;
            }
        }

        private void ClearBackgroundProperty()
        {
            FirstNameTextBox.ClearValue(BackgroundProperty);
            LastNameTextBox.ClearValue(BackgroundProperty);
            TravelTimeTextBox.ClearValue(BackgroundProperty);
            CountryComboBoxTextBox.ClearValue(BackgroundProperty);
            TravelPreferenceTextBox.ClearValue(BackgroundProperty);
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            ClearBackgroundProperty();
            FirstNameTextBox.Clear();
            LastNameTextBox.Clear();
            TravelTimeTextBox.Clear();
            CountryComboBoxTextBox.Clear();
            TravelPreferenceTextBox.Clear();
        }
    }
}

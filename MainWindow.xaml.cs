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














        private Grammar CreateGrammarFromWords()
        {
            GrammarBuilder grammarBuilder = new GrammarBuilder();
            List<string> words = new List<string> { "PIOTR"
                ,"KRZYSZTOF"
                ,"ANDRZEJ"
                ,"TOMASZ"
                ,"JAN"
                ,"PAWEŁ"
                ,"MICHAŁ"
                ,"MARCIN"
                ,"STANISŁAW"
                ,"JAKUB"
                ,"ADAM"
                ,"MAREK"
                ,"ŁUKASZ"
                ,"GRZEGORZ"
                ,"MATEUSZ"
                ,"WOJCIECH"
                ,"MARIUSZ"
                ,"DARIUSZ"
                ,"ZBIGNIEW"
                ,"JERZY"
                ,"MACIEJ"
                ,"RAFAŁ"
                ,"ROBERT"
                ,"KAMIL"
                ,"JÓZEF"
                ,"JACEK"
                ,"DAWID"
                ,"TADEUSZ"
                ,"RYSZARD"
                ,"SZYMON"
                ,"KACPER"
                ,"BARTOSZ"
                ,"JANUSZ"
                ,"JAROSŁAW"
                ,"SŁAWOMIR"
                ,"MIROSŁAW"
                ,"ARTUR"
                ,"HENRYK"
                ,"SEBASTIAN"
                ,"DAMIAN"
                ,"PATRYK"
                ,"KAZIMIERZ"
                ,"PRZEMYSŁAW"
                ,"DANIEL"
                ,"KAROL"
                ,"ROMAN"
                ,"MARIAN"
                ,"WIESŁAW"
                ,"FILIP",
                //nazwiska
                "NOWAK","KOWALSKI","WIŚNIEWSKI","WÓJCIK","KOWALCZYK","KAMIŃSKI","LEWANDOWSKI","ZIELIŃSKI","DĄBROWSKI","SZYMAŃSKI","WOŹNIAK","KOZŁOWSKI","MAZUR","JANKOWSKI","KWIATKOWSKI","WOJCIECHOWSKI","KRAWCZYK","KACZMAREK","PIOTROWSKI","GRABOWSKI","ZAJĄC","PAWŁOWSKI","MICHALSKI","KRÓL","NOWAKOWSKI","WRÓBEL","WIECZOREK","JABŁOŃSKI","DUDEK","ADAMCZYK","MAJEWSKI","NOWICKI","OLSZEWSKI","STĘPIEŃ","MALINOWSKI","JAWORSKI","PAWLAK","GÓRSKI","SIKORA","WITKOWSKI","WALCZAK","RUTKOWSKI","BARAN","MICHALAK","SZEWCZYK","OSTROWSKI","TOMASZEWSKI","DUDA","ZALEWSKI","PIETRZAK","WRÓBLEWSKI","JASIŃSKI","MARCINIAK","ZAWADZKI","SADOWSKI","BĄK","JAKUBOWSKI","WILK","CHMIELEWSKI","WŁODARCZYK","BORKOWSKI","SOKOŁOWSKI","SZCZEPAŃSKI","SAWICKI","LIS","KUCHARSKI","MAZUREK","KUBIAK","KALINOWSKI","WYSOCKI","MACIEJEWSKI","CZARNECKI","KOŁODZIEJ","KAŹMIERCZAK","URBAŃSKI","SOBCZAK","KONIECZNY","GŁOWACKI","KRUPA","WASILEWSKI","ZAKRZEWSKI","KRAJEWSKI","SIKORSKI","ADAMSKI","MRÓZ","LASKOWSKI","GAJEWSKI","ZIÓŁKOWSKI","SZULC","MAKOWSKI","KOZAK","BARANOWSKI","SZYMCZAK","KACZMARCZYK","PRZYBYLSKI","BRZEZIŃSKI","CZERWIŃSKI","BŁASZCZYK","ANDRZEJEWSKI","BOROWSKI","CIEŚLAK","KANIA","CHOJNACKI","GÓRECKI","SZCZEPANIAK","JANIK","LESZCZYŃSKI","LIPIŃSKI","MUCHA","KOZIOŁ","WESOŁOWSKI","KOWALEWSKI","MIKOŁAJCZYK","NOWACKI","DOMAŃSKI","JAROSZ","CIESIELSKI","MUSIAŁ","KOWALIK","ZIĘBA","KOŁODZIEJCZYK","MARKOWSKI","KOPEĆ","BRZOZOWSKI","PIĄTEK","KUREK","ŻAK","ORŁOWSKI","PAWLIK","TOMCZYK","TOMCZAK","KOT","URBANIAK","WAWRZYNIAK","MARKIEWICZ","WÓJTOWICZ","KRUK","WOLSKI","POLAK","CZAJKOWSKI","STASIAK","NAWROCKI","SOWA","STANKIEWICZ","ŁUCZAK","JASTRZĘBSKI","KARPIŃSKI","DZIEDZIC","WIERZBICKI","PIASECKI",
                //kraje
                "Afganistan","Albania","Algieria","Andora","Angola","Antigua i Barbuda","Arabia Saudyjska","Argentyna","Armenia","Australia","Austria","Azerbejdżan","Bahamy","Bahrajn","Bangladesz","Barbados","Belgia","Belize","Benin","Bhutan","Białoruś","Boliwia","Bośnia i Hercegowina","Botswana","Brazylia","Brunei","Bułgaria","Burkina Faso","Burundi","Chile","Chiny","Chorwacja","Cypr","Czad","Czarnogóra","Czechy","Dania","Demokratyczna Republika Konga","Dominika","Dominikana","Dżibuti","Egipt","Ekwador","Erytrea","Estonia","Eswatini","Etiopia","Fidżi","Filipiny","Finlandia","Francja","Gabon","Gambia","Ghana","Grecja","Grenada","Gruzja","Gujana","Gwatemala","Gwinea","Gwinea Bissau","Gwinea Równikowa","Haiti","Hiszpania","Holandia","Honduras","Indie","Indonezja","Irak","Iran","Irlandia","Islandia","Izrael","Jamajka","Japonia","Jemen","Jordania","Kambodża","Kamerun","Kanada","Katar","Kazachstan","Kenia","Kirgistan","Kiribati","Kolumbia","Komory","Kongo","Korea Południowa","Korea Północna","Kostaryka","Kuba","Kuwejt","Laos","Lesotho","Liban","Liberia","Libia","Liechtenstein","Litwa","Luksemburg","Łotwa","Macedonia Północna","Madagaskar","Malawi","Malediwy","Malezja","Mali","Malta","Maroko","Mauretania","Mauritius","Meksyk","Mikronezja","Mjanma","Mołdawia","Monako","Mongolia","Mozambik","Namibia","Nauru","Nepal","Niemcy","Niger","Nigeria","Nikaragua","Norwegia","Nowa Zelandia","Oman","Pakistan","Palau","Panama","Papua-Nowa Gwinea","Paragwaj","Peru","Polska","Południowa Afryka","Portugalia","Republika Środkowoafrykańska","Republika Zielonego Przylądka","Rosja","Rumunia","Rwanda","Saint Kitts i Nevis","Saint Lucia","Saint Vincent i Grenadyny","Salwador","Samoa","San Marino","Senegal",
                // czas trwania podrozy
                "0","1","2","3","4","5","6","7","8","9", "dni", "tygodnie", "miesiące",
                //preferencje podroznicze
                "Przygoda","Kultura","Relaks","Edukacja","Luksus",
                "Wyślij formularz"
            };
            grammarBuilder.Append(new Choices(words.ToArray()));
            Grammar grammar = new Grammar(grammarBuilder);
            return grammar;
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

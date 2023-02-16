using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using WpfAnimatedGif;

namespace Translater
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Btnswitching_Click(object sender, RoutedEventArgs e)
        {
            string lang;
            lang = Lang_1.Text;
            Lang_1.Text = Lang_2.Text;
            Lang_2.Text = lang;
            RusTxt.Text = EnglTxt.Text;
            EnglTxt.Text = "Переведенный текст";
        }
        private async Task<string> translateAsync(string perevodText, string LngPer, string LngPer2)
        {
            string text = perevodText;
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://google-translate1.p.rapidapi.com/language/translate/v2"),
                Headers =
                {
                    { "X-RapidAPI-Key", "6749f40f3dmsh789e82639157c05p153452jsnb78f0977791b" },
                    { "X-RapidAPI-Host", "google-translate1.p.rapidapi.com" },
                },
                Content = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    { "q", text },
                    { "target", LngPer2 },
                    { "source", LngPer },
                }),
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                string body = await response.Content.ReadAsStringAsync();
                string perevod = body.Remove(0, 44);
                char[] MyChar = { '"', ']', '}' };
                perevod = perevod.TrimEnd(MyChar);
                EnglTxt.Text = perevod;
                return body;
            }
        }

        private void BtnTranslate_Click(object sender, RoutedEventArgs e)
        {
            string perevod;
            if ((Lang_1.Text == string.Empty) && (Lang_2.Text == string.Empty))
                MessageBox.Show("Язык для перевода не выбран", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            else
            {
                if (Lang_1.Text == string.Empty)
                    MessageBox.Show("Язык c которого  переводить не выбран", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                else
                {
                    if (Lang_2.Text == string.Empty)
                        MessageBox.Show("Язык на который переводить не выбран", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    else
                    {
                        if (Lang_1.Text == Lang_2.Text)
                        {
                            MessageBox.Show("Выбраны одинаковые языки", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        else
                        {
                            if (RusTxt.Text == string.Empty)
                                MessageBox.Show("Текст для перевода не введен", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                            else
                            {
                                string LngPer, LngPer2;
                                if (Lang_1.Text == "Русский")
                                {
                                    LngPer = "ru";
                                    LngPer2 = "en";
                                }
                                else
                                {
                                    LngPer = "en";
                                    LngPer2 = "ru";
                                }
                                perevod = RusTxt.Text;
                                translateAsync(perevod, LngPer, LngPer2);
                            }
                        }
                    }
                }
            }
        }
    }
}

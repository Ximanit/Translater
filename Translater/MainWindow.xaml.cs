using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Translater
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string lang;
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
                    { "X-RapidAPI-Key", "90157119cemshf5ab74c158bc779p1e15b8jsne911ab0980bd" },
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
    }
}

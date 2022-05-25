using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
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
using System.Xml.Linq;

namespace App_1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //record Rate(string Code, string Currency, double Bid, double Ask);
        record Rate(string Code, string Currency, double Bid, double Ask)
        {
            [JsonPropertyName("code")]
            public string Code { get; set; }
            [JsonPropertyName("currency")]
            public string Currency { get; set; }
            [JsonPropertyName("bid")]
            public decimal Bid { get; set; }
            [JsonPropertyName("ask")]
            public decimal Ask { get; set; }
        };
        Dictionary<string, Rate> Rates = new Dictionary<string, Rate>();

        class RateTable
        {
            [JsonPropertyName("table")]
            public string Table { get; set; }
            [JsonPropertyName("no")]
            public string Number { get; set; }
            [JsonPropertyName("tradingDate")]
            public DateTime TradingDate { get; set; }
            [JsonPropertyName("effectiveDate")]
            public DateTime EffectiveDate { get; set; }
            public List<Rate> Rates { get; set; }
        }
        private void DownloadDateJson()
        {
            WebClient client = new WebClient();
            client.Headers.Add("Accept", "application/json");
            string json = client.DownloadString("http://api.nbp.pl/api/exchangerates/tables/C");
            RateTable rateTable = JsonSerializer.Deserialize<List<RateTable>>(json)[0];
            rateTable.Rates.Add(new Rate("PLN", "złoty", 1, 1));
        }
        public void DownloadData()
        {
            WebClient client = new WebClient();
            client.Headers.Add("Accept", "application/xml");
            string xml = client.DownloadString("http://api.nbp.pl/api/exchangerates/tables/C");
            XDocument doc = XDocument.Parse(xml);
            List<Rate> list = doc.Elements("ArrayOfExchangeRatesTable")
                .Elements("ExchangeRatesTable")
                .Elements("Rates")
                .Elements("Rate")
                .Select(node => new Rate(
                 node.Element("Code").Value,
                 node.Element("Currency").Value,
                 0,//double.Parse(node.Element("Bid").Value),
                 0//double.Parse(node.Element("Ask").Value)
                )).ToList();

            foreach(Rate r in list)
            {
                Rates.Add(r.Code, r);
            }
        }
        public MainWindow()
        {
            InitializeComponent();
            DownloadData();

            foreach(string code in Rates.Keys)
            {
                InputCurrencyCode.Items.Add(code);
                ResultCurrencyCode.Items.Add(code);
            }
            //InputCurrencyCode.Items.Add("USD");
            //InputCurrencyCode.Items.Add("PLN");
            //InputCurrencyCode.Items.Add("EUR");
            //ResultCurrencyCode.Items.Add("USD");
            //ResultCurrencyCode.Items.Add("PLN");
            //ResultCurrencyCode.Items.Add("EUR");
            //InputCurrencyCode.SelectedIndex = 1;
            //ResultCurrencyCode.SelectedIndex = 0;

        }

        

        private void CalcResult(object sender, RoutedEventArgs e)
        {
            string inputCode = (string) InputCurrencyCode.SelectedItem;
            string resultCode = (string)ResultCurrencyCode.SelectedItem;
            string amountStr = InputValue.Text;
            MessageBox.Show($"Wybrany kod wejściowy {inputCode}\nWybrany kod wyjściowy {resultCode}\nKwota: {amountStr}");
        }

        private void NumberValidation(object sender, TextCompositionEventArgs e)
        {
            string s = e.Text;
            if (s.EndsWith(","))
            {
                s += ("0");
            }
            e.Handled = !decimal.TryParse(s, out decimal value);
        }
    }
}

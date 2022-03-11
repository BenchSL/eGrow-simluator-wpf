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
using System.Windows.Shapes;
using eGrow_simulator.Class;
using System.Net.Http.Headers;
using System.Text.Json;

namespace eGrow_simulator
{
    /// <summary>
    /// Interaction logic for DetectSensorError.xaml
    /// </summary>
    public partial class DetectSensorError : Window
    {
        private static readonly HttpClient client = new HttpClient();

        private static async Task<List<Error>> GetErrors()
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var stringError = await client.GetStreamAsync(""); //url call to API
            var SeriaError = await JsonSerializer.DeserializeAsync<List<Error>>(stringError); //converting json to List<Error>
            return SeriaError;
        }

        private static async Task<Error> GetRandomError(string errCode)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var stringError = await client.GetStreamAsync("" + errCode); //url call to API
            var SeriaError = await JsonSerializer.DeserializeAsync<Error>(stringError); //converting json to List<Error>
            return SeriaError;
        }

        private string GenerateRandomCode() //codes can be 404, 501, 604 or 705
        {
            string[] CodesArr = new string[4] { "404", "501", "604", "705"};
            string randomCode = string.Empty;
            Random rnd = new Random();
            int index = rnd.Next(CodesArr.Length);
            randomCode = CodesArr[index];
            return randomCode;
        }

        public DetectSensorError()
        {
            InitializeComponent();
        }
    }
}

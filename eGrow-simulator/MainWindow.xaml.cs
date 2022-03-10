using eGrow_simulator.Class;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
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

namespace eGrow_simulator
{
    public partial class MainWindow : Window
    {
        //Za klicanje API-ja
        HttpClient client = new HttpClient();

        //Samo zdej !
        // REMOVE LATER
        //TO DO: Dodaj login, shranjevanje "id" izbrane rastline
        //ki ga dobi iz prejšnega "home" (preko konstruktorja !)
        public int IdRastline = 3;

        //Preverja, če je simulacija aktivna
        public bool SimulacijaAktivna = false;

        //Shranjevanje dobljenih vrednosti
        //(Konstantno updajtanje)
        public SensorData SensorData;


        public MainWindow()
        {
            InitializeComponent();
            //Zažene ob začetku
            OnStartup();
        }

        #region OnStartup funkcija
        public void OnStartup()
        {
            client.BaseAddress = new Uri("https://localhost:44319/api/");
            client.DefaultRequestHeaders.Accept.Clear();

            LblStanjeSimulacije.Content = "Offline";
            LblStanjeSimulacije.Foreground = Brushes.Red;
        }
        #endregion


        #region Simulacija (začetek/konec)
        private void BtnSimuliraj_Click(object sender, RoutedEventArgs e)
        {
            //Nastavljanje "timer" 
            var timer = new System.Timers.Timer();
            timer.Interval = 1000;
            timer.Elapsed += OnTimerElapsed;

            if (SimulacijaAktivna == true)
            {

                timer.Stop();
                timer.Enabled = false;

                LblStanjeSimulacije.Content = "Offline";
                LblStanjeSimulacije.Foreground = Brushes.Red;
                BtnSimuliraj.Content = "Simuliraj";
                SimulacijaAktivna = false;

                LblTempPrsti.Content = "";
                LblTempProstora.Content = "";
                LblVlagaPrsti.Content = "";
                LblVlagaProstora.Content = "";
                LblUvIndex.Content = "";
                LblSoncnoSevanje.Content = "";
                LblVlagaListov.Content = "";
                LblRast.Content = "";


            }
            else if (SimulacijaAktivna == false)
            {
                LblStanjeSimulacije.Content = "Online";
                LblStanjeSimulacije.Foreground = Brushes.Green;
                BtnSimuliraj.Content = "Stop";
                SimulacijaAktivna = true;

                timer.Start();
                timer.Enabled = true;


            }
        }
        #endregion

        #region Timer (Vsako sekundo se izvede)
        private async void OnTimerElapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            var Response = await client.GetStringAsync("SensorData/" + IdRastline);
            SensorData = JsonConvert.DeserializeObject<SensorData>(Response);
            this.Dispatcher.Invoke(() =>
            {
                LblTempPrsti.Content = SensorData.SoiltemperatureCelsius + " °C".ToString();
                LblTempProstora.Content = SensorData.AmbientTemperatureCelsius + " °C".ToString();
                LblVlagaPrsti.Content = SensorData.SoilHumidityPercentage + " G.m -3".ToString();
                LblVlagaProstora.Content = SensorData.AmbientHumidityPercentage + " G.m -3".ToString();
                LblUvIndex.Content = SensorData.UvIndex + " nm".ToString();
                LblSoncnoSevanje.Content = SensorData.SolarRadiation + " w/m²".ToString();
                LblVlagaListov.Content = SensorData.LeafWetnes + " G.m -3".ToString();
                LblRast.Content = SensorData.GrowthCm + " cm".ToString();
            });



        }
        #endregion

        #region Zapiranje simulatorja
        private void BtnZapri_Click(object sender, RoutedEventArgs e)
        {//Zapiranje aplikacije
            Application.Current.Shutdown();
            this.Close();
        }
        #endregion

        #region Shranjevanje nastavitev
        private void BtnShrani_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(InputTempProstora.Value.ToString(), out double value1))
            {
                SensorData.AmbientTemperatureCelsius = value1;
            }

            if (double.TryParse(InputTempPrst.Value.ToString(), out double value2))
            {

            }

            //if (double.TryParse(InputUvIndex.Value.ToString(), out double value3))
            //{

            //}
        }
        #endregion

    }
}

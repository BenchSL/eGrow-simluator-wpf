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
        public bool Stopping = false;

        //Shranjevanje dobljenih vrednosti za senzor
        //(Konstantno updajtanje)
        public Senzor PodatkiSenzorja;

        //Shranjevanje dobljenih vrednosti za napravoi
        //(Konstantno updajtanje)
        public Naprava PodatkiNaprave;

        //Nastavljanje "timer" 
        public System.Timers.Timer timer = new System.Timers.Timer();

        public MainWindow()
        {
            InitializeComponent();
            //Zažene ob začetku
            OnStartup();
        }

        #region OnStartup funkcija
        public void OnStartup()
        {
            client.BaseAddress = new Uri("https://localhost:44319/");
            client.DefaultRequestHeaders.Accept.Clear();

            LblStanjeSimulacije.Content = "Offline";
            LblStanjeSimulacije.Foreground = Brushes.Red;
        }
        #endregion

        #region Simulacija (začetek/konec)
        private void BtnSimuliraj_Click(object sender, RoutedEventArgs e)
        {
            timer.Interval = 1000;
            timer.Elapsed += OnTimerElapsed;

            if (SimulacijaAktivna == true)
            {
                Stopping = true;
            }
            if (SimulacijaAktivna == false)
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
            var Response1 = await client.GetStringAsync("api/SensorData/" + IdRastline);
            PodatkiSenzorja = JsonConvert.DeserializeObject<Senzor>(Response1);

            var Response2 = await client.GetStringAsync("api/Device/" + IdRastline);
            PodatkiNaprave = JsonConvert.DeserializeObject<Naprava>(Response2);

            this.Dispatcher.Invoke(() =>
            {
                LblTempPrsti.Content = PodatkiSenzorja.SoiltemperatureCelsius + " °C".ToString();
                LblTempProstora.Content = PodatkiSenzorja.AmbientTemperatureCelsius + " °C".ToString();
                LblVlagaPrsti.Content = PodatkiSenzorja.SoilHumidityPercentage + " G.m -3".ToString();
                LblVlagaProstora.Content = PodatkiSenzorja.AmbientHumidityPercentage + " G.m -3".ToString();
                LblUvIndex.Content = PodatkiSenzorja.UvIndex + " nm".ToString();
                LblSoncnoSevanje.Content = PodatkiSenzorja.SolarRadiation + " w/m²".ToString();
                LblVlagaListov.Content = PodatkiSenzorja.LeafWetnes + " G.m -3".ToString();
                LblRast.Content = PodatkiSenzorja.GrowthCm + " cm".ToString();

                LblKolicinaVode.Content = PodatkiNaprave.RavenVodeRezorvarja + " ml".ToString();
                LblKolicinaGnojila.Content = PodatkiNaprave.RavenGnojilaRezorvarja + " ml".ToString();
            });

            if (Stopping == true)
            {

                SimulacijaAktivna = false;
                Stopping = false;
                timer.Stop();
                timer.Enabled = false;


            }

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
        private async void BtnShrani_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(InputTempPrst.Value.ToString(), out double value1))
            {
                PodatkiSenzorja.SoiltemperatureCelsius = (double)InputTempPrst.Value;
            }
            if (double.TryParse(InputTempProstora.Value.ToString(), out double value2))
            {
                PodatkiSenzorja.AmbientTemperatureCelsius = (double)InputTempProstora.Value;
            }
            if (double.TryParse(InputVlagaPrsti.Value.ToString(), out double value3))
            {
                PodatkiSenzorja.SoilHumidityPercentage = (int)InputVlagaPrsti.Value;
            }
            if (double.TryParse(InputVlagaProstora.Value.ToString(), out double value4))
            {
                PodatkiSenzorja.AmbientHumidityPercentage = (int)InputVlagaProstora.Value;
            }
            if (double.TryParse(InputDodajGnojilo.Value.ToString(), out double value5))
            {
                PodatkiNaprave.RavenGnojilaRezorvarja = PodatkiNaprave.RavenGnojilaRezorvarja + (int)InputDodajGnojilo.Value;
            }
            if (double.TryParse(InputDodajVodo.Value.ToString(), out double value6))
            {
                PodatkiNaprave.RavenVodeRezorvarja = PodatkiNaprave.RavenVodeRezorvarja +  (int)InputDodajVodo.Value;
            }

            await client.PutAsJsonAsync("api/SensorData/" + IdRastline, PodatkiSenzorja);
            await client.PutAsJsonAsync("api/Device/" + IdRastline, PodatkiSenzorja);

        }
        #endregion

    }
}

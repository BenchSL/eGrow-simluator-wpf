using System;
using System.Collections.Generic;
using System.Linq;
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

namespace eGrow_simulator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public bool PrizganaLucka = false;
        public bool PrizganGrelec = false;

        public MainWindow()
        {
            InitializeComponent();

            LblGrelecStatus.Foreground = Brushes.Red;
            LblLuckeStatus.Foreground = Brushes.Red;
            LblGrelecStatus.Content = "Off";
            LblLuckeStatus.Content = "Off";

        }

        private void BtnSimuliraj_Click(object sender, RoutedEventArgs e)
        {
            RegLog hw = new RegLog();
            hw.ShowDialog();
        }

        private void BtnZapri_Click(object sender, RoutedEventArgs e)
        {//Zapiranje aplikacije
            Application.Current.Shutdown(); 
            this.Close();   
        }

        private void BtnPrizgiLuc_Click(object sender, RoutedEventArgs e)
        {
            if (PrizganaLucka == true)
            {
                LblLuckeStatus.Foreground = Brushes.Red;
                LblLuckeStatus.Content = "Off";

                PrizganaLucka = false;

            }
            else if (PrizganaLucka == false)
            {
                LblLuckeStatus.Foreground = Brushes.Green;
                LblLuckeStatus.Content = "On";

                PrizganaLucka = true;
            }

        }

        private void BtnGrelec_Click(object sender, RoutedEventArgs e)
        {
            if (PrizganGrelec == true)
            {
                LblGrelecStatus.Foreground = Brushes.Red;
                LblGrelecStatus.Content = "Off";

                PrizganGrelec = false;

            }
            else if (PrizganGrelec == false)
            {
                LblGrelecStatus.Foreground = Brushes.Green;
                LblGrelecStatus.Content = "On";

                PrizganGrelec = true;
            }
        }
    }
}

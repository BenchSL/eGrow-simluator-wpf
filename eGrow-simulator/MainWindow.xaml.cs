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
        public MainWindow()
        {
            InitializeComponent();



            //Na default sta lučka in grelec izklopljena (skrita) 
            //Spodaj v kodi ju lahko primerno vklapljamo !
            ImgPodGrelec.Visibility = Visibility.Hidden;
            ImgPodLucke.Visibility = Visibility.Hidden;
        }




        private void BtnSimuliraj_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnZapri_Click(object sender, RoutedEventArgs e)
        {//Zapiranje aplikacije
            Application.Current.Shutdown(); 
            this.Close();   
        }
    }
}

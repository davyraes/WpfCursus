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

namespace verkeer
{
    /// <summary>
    /// Interaction logic for verkeer.xaml
    /// </summary>
    public partial class Verkeerslicht : Window
    {
        public Verkeerslicht()
        {
            InitializeComponent();
        }

        private void ButtonGO_Click(object sender, RoutedEventArgs e)
        {
            OranjeLamp.Opacity = 0;
            GroeneLamp.Opacity = 100;
            ButtonGO.IsEnabled = false;
            ButtonOpgelet.IsEnabled = true;
        }

        private void ButtonStop_Click(object sender, RoutedEventArgs e)
        {
            OranjeLamp.Opacity = 0;
            RodeLamp.Opacity = 100;
            ButtonOpgelet.IsEnabled = true;
            ButtonStop.IsEnabled = false;
        }

        private void ButtonOpgelet_Click(object sender, RoutedEventArgs e)
        {
            OranjeLamp.Opacity = 100;
            ButtonOpgelet.IsEnabled = false;
            if(GroeneLamp.Opacity==100)
            {
                GroeneLamp.Opacity = 0;
                ButtonStop.IsEnabled = true;
            }
            else if(RodeLamp.Opacity==100)
            {
                RodeLamp.Opacity = 0;
                ButtonGO.IsEnabled = true;
            }
        }
    }
}

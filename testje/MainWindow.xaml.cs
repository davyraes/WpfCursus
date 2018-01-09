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

namespace testje
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void cb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cb.SelectedValue != null)
            {
                BrushConverter bc = new BrushConverter();
                SolidColorBrush blauw = (SolidColorBrush)bc.ConvertFromString(cb.SelectedValue.ToString());
                Application.Current.MainWindow.Background = blauw;
            }
        }
    }
}

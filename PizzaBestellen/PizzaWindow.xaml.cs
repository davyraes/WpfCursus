using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PizzaBestellen
{
    /// <summary>
    /// Interaction logic for PizzaWindow.xaml
    /// </summary>
    public partial class PizzaWindow : Window
    {
        public PizzaWindow()
        {
            InitializeComponent();
        }

        private void ButtonHoeveelheidPlus_Click(object sender, RoutedEventArgs e)
        {
            if (Convert.ToInt16 (LabelHoeveelheid.Content) < 10)
                LabelHoeveelheid.Content = Convert.ToInt16(LabelHoeveelheid.Content)+1;
        }

        private void ButtonHoeveelheidMin_Click(object sender, RoutedEventArgs e)
        {
            if (Convert.ToInt16(LabelHoeveelheid.Content) > 1)
                LabelHoeveelheid.Content = Convert.ToInt16(LabelHoeveelheid.Content)-1;
        }

        private void ButtonBestellen_Click(object sender, RoutedEventArgs e)
        {
            var bestelling = new StringBuilder();
            bestelling.Append($"u heeft {LabelHoeveelheid.Content} ");
            foreach (RadioButton button in StackPanelGrootte.Children)
                if (button.IsChecked == true)
                    bestelling.Append($"{button.Content} pizza('s) besteld met:");
            foreach (CheckBox box in StackPanelIngredienten.Children)
                if (box.IsChecked == true)
                    bestelling.Append($"{box.Tag}, ");
            bestelling.Remove(bestelling.Length - 2, 1);
            bestelling.AppendLine();
            foreach (ToggleButton button in StackPanelExtras.Children)
                if (button.IsChecked == true)
                    bestelling.Append($"{ button.Tag} ");
            LabelBestelling.Content = bestelling.ToString();
        }
        
    }
}

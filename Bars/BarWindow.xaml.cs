using System;
using Microsoft.Win32;
using System.ComponentModel;
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

namespace Bars
{
    /// <summary>
    /// Interaction logic for BarWindow.xaml
    /// </summary>
    public partial class BarWindow : Window
    {
        public static RoutedCommand mijnRouteCtrlB = new RoutedCommand();
        public static RoutedCommand mijnRouteCtrlI = new RoutedCommand();
        public BarWindow()
        {
            InitializeComponent();
            //CommandBinding mijnCtrlB =
            //    new CommandBinding(mijnRouteCtrlB, ctrlBexecuted);
            //this.CommandBindings.Add(mijnCtrlB);
            //KeyGesture toetsCtrlB = new KeyGesture(Key.B, ModifierKeys.Control);
            //KeyBinding mijnkeyCtrlB = new KeyBinding(mijnRouteCtrlB, toetsCtrlB);
            //this.InputBindings.Add(mijnkeyCtrlB);

            //CommandBinding mijnCtrlI =
            //    new CommandBinding(mijnRouteCtrlI, ctrlIexecuted);
            //this.CommandBindings.Add(mijnCtrlI);
            //KeyGesture toetsCtrlI = new KeyGesture(Key.I, ModifierKeys.Control);
            //KeyBinding mijnkeyCtrlI = new KeyBinding(mijnRouteCtrlI, toetsCtrlI);
            //this.InputBindings.Add(mijnkeyCtrlI);
        }
        private void ctrlBexecuted(object sender,ExecutedRoutedEventArgs e)
        {
            Vet_aan_uit();
        }
        private void ctrlIexecuted(object sender,ExecutedRoutedEventArgs e)
        {
            Schuin_aan_uit();
        }

        private void Lettertype_Click(object sender, RoutedEventArgs e)
        {
            MenuItem lettertype = (MenuItem)sender;
            foreach (MenuItem huidig in Fontjes.Items)
                huidig.IsChecked = false;
            lettertype.IsChecked = true;
            LetterTypeComboBox.SelectedItem = new FontFamily(lettertype.Header.ToString());
        }
        
        private void MenuVet_Click(object sender, RoutedEventArgs e)
        {
            Vet_aan_uit();
        }

        private void MenuSchuin_Click(object sender, RoutedEventArgs e)
        {
            Schuin_aan_uit();
        }
        private void Vet_aan_uit()
        {
            if (TextBoxVoorbeeld.FontWeight==FontWeights.Normal)
            {
                TextBoxVoorbeeld.FontWeight = FontWeights.Bold;
                MenuVet.IsChecked = true;
                ButtonVet.IsChecked = true;
            }
            else
            {
                TextBoxVoorbeeld.FontWeight = FontWeights.Normal;
                MenuVet.IsChecked = false;
                ButtonVet.IsChecked = false;
            }
        }
        private void Schuin_aan_uit()
        {
            if (TextBoxVoorbeeld.FontStyle == FontStyles.Normal)
            {
                TextBoxVoorbeeld.FontStyle = FontStyles.Italic;
                MenuSchuin.IsChecked = true;
                ButtonSchuin.IsChecked = true;
            }
            else
            {
                TextBoxVoorbeeld.FontStyle = FontStyles.Normal;
                MenuSchuin.IsChecked = false;
                ButtonSchuin.IsChecked = false;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LetterTypeComboBox.Items.SortDescriptions.Add(new SortDescription("Source",ListSortDirection.Ascending));
            LetterTypeComboBox.SelectedItem = new FontFamily(TextBoxVoorbeeld.FontFamily.ToString());
        }

        private void LetterTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (MenuItem huidig in Fontjes.Items)
                if (LetterTypeComboBox.SelectedItem.ToString() == huidig.Header.ToString())
                    huidig.IsChecked = true;
                else
                    huidig.IsChecked = false;
        }
    }
}

using System;
using System.Media;
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

namespace Telefoon
{
    /// <summary>
    /// Interaction logic for TelefoonWindow.xaml
    /// </summary>
    public partial class TelefoonWindow : Window
    {
        public List<Persoon> personen = new List<Persoon>();
        public TelefoonWindow()
        {
            InitializeComponent();
        }

        private void ComboBoxGroep_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBoxPersonen.Items.Clear();
            foreach (Persoon persoon in personen)
                if (persoon.Groep == ComboBoxGroep.SelectedItem.ToString() || ComboBoxGroep.SelectedIndex == 0)
                    ListBoxPersonen.Items.Add(persoon);
            ListBoxPersonen.Items.SortDescriptions.Add(new SortDescription("Naam", ListSortDirection.Ascending));
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            personen.Add(new Persoon("anne", "012345678", "Vrienden", new BitmapImage(new Uri(@"Images/anne.jpg",UriKind.Relative))));
            personen.Add( new Persoon("bob", "123456789", "Vrienden", new BitmapImage(new Uri(@"Images/bob.jpg",UriKind.Relative))));
            personen.Add( new Persoon("ed","234567890","Vrienden",new BitmapImage(new Uri(@"Images/ed.jpg",UriKind.Relative))));
            personen.Add( new Persoon("collega1","345678901","Werk",new BitmapImage(new Uri(@"Images/collega1.jpg",UriKind.Relative))));
            personen.Add( new Persoon("collega2","456789012","Werk",new BitmapImage(new Uri(@"Images/collega2.jpg",UriKind.Relative))));
            personen.Add( new Persoon("collega3","567890123","Werk",new BitmapImage(new Uri(@"Images/collega3.jpg",UriKind.Relative))));
            personen.Add( new Persoon("Grote Zus","678901234","Familie",new BitmapImage(new Uri(@"Images/grotezus.jpg",UriKind.Relative))));
            personen.Add( new Persoon("Kleine Zus","789012345","Familie",new BitmapImage(new Uri(@"Images/kleinezus.jpg",UriKind.Relative))));
            personen.Add( new Persoon("Tante Non","890123456","Familie",new BitmapImage(new Uri(@"Images/tantenon.jpg",UriKind.Relative))));
            personen.Add( new Persoon("Vader","901234567","Familie",new BitmapImage(new Uri(@"Images/vader.jpg",UriKind.Relative))));
            ComboBoxGroep.Items.Add("-Alle Groepen-");
            ComboBoxGroep.Items.Add("Vrienden");
            ComboBoxGroep.Items.Add("Werk");
            ComboBoxGroep.Items.Add("Familie");
            ComboBoxGroep.SelectedIndex = 0;
        }

        private void ButtonTelefoon_Click(object sender, RoutedEventArgs e)
        {
            if (ListBoxPersonen.SelectedItem == null)
                MessageBox.Show("Je moet eerst iemand selecteren", "Niemand gekozen", MessageBoxButton.OK, MessageBoxImage.Warning);
            else
            {
                Persoon contact = (Persoon)ListBoxPersonen.SelectedItem;
                if(MessageBox.Show($"Wil je {contact.Naam} bellen \nop het nummer: {contact.TelefoonNr}", "Telefoon", MessageBoxButton.YesNo, MessageBoxImage.Question)==MessageBoxResult.Yes)
                {
                    SoundPlayer speler = new SoundPlayer("PHONE.wav");
                    speler.Play();
                    MessageBox.Show("Aan het bellen", "bellen", MessageBoxButton.OK,MessageBoxImage.Information);
                }
            }
        }
    }
}

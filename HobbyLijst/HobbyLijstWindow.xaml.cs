using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HobbyLijst
{
    /// <summary>
    /// Interaction logic for HobbyLijstWindow.xaml
    /// </summary>
    public partial class HobbyLijstWindow : Window
    {
        public HobbyLijstWindow()
        {
            
            InitializeComponent();
        }
        public List<Hobby> hobbies = new List<Hobby>();
        private void Window_loaded(object sender,RoutedEventArgs e)
        {
            hobbies.Add(new Hobby("sport", "voetbal", new BitmapImage(new Uri(@"Images/voetbal.jpg", UriKind.Relative))));
            hobbies.Add(new Hobby("sport", "atletiek", new BitmapImage(new Uri(@"Images/atletiek.jpg", UriKind.Relative))));
            hobbies.Add(new Hobby("sport", "basketbal", new BitmapImage(new Uri(@"Images/basketbal.jpg", UriKind.Relative))));
            hobbies.Add(new Hobby("sport", "tennis", new BitmapImage(new Uri(@"Images/tennis.jpg", UriKind.Relative))));
            hobbies.Add(new Hobby("sport", "turnen", new BitmapImage(new Uri(@"Images/turnen.jpg", UriKind.Relative))));
            hobbies.Add(new Hobby("muziek", "trompet", new BitmapImage(new Uri(@"Images/trompet.jpg", UriKind.Relative))));
            hobbies.Add(new Hobby("muziek", "drum", new BitmapImage(new Uri(@"Images/drum.jpg", UriKind.Relative))));
            hobbies.Add(new Hobby("muziek", "gitaar", new BitmapImage(new Uri(@"Images/gitaar.jpg", UriKind.Relative))));
            hobbies.Add(new Hobby("muziek", "piano", new BitmapImage(new Uri(@"Images/piano.jpg", UriKind.Relative))));
            comboBoxCategorie.Items.Add("- alle categoriën -");
            comboBoxCategorie.Items.Add("muziek");
            comboBoxCategorie.Items.Add("sport");
            comboBoxCategorie.SelectedIndex = 0;
        }

        private void comboBoxCategorie_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBoxHobbies.Items.Clear();
            foreach(Hobby hob in hobbies)
            {
                if (hob.Categorie == comboBoxCategorie.SelectedItem.ToString() || comboBoxCategorie.SelectedIndex == 0)
                    ListBoxHobbies.Items.Add(hob);
            }
            ListBoxHobbies.Items.SortDescriptions.Add(new System.ComponentModel.SortDescription("activiteit", System.ComponentModel.ListSortDirection.Ascending));
        }

        private void ButtonHobbys_Click(object sender, RoutedEventArgs e)
        {
            if (ListBoxHobbies.SelectedItem!=null)
            {
                Hobby gekozenHobby = (Hobby)ListBoxHobbies.SelectedItem;
                ListBoxGekozen.Items.Add(gekozenHobby);
                ListBoxGekozen.Items.SortDescriptions.Add(new SortDescription("Categorie",ListSortDirection.Ascending));
                ListBoxGekozen.Items.SortDescriptions.Add(new SortDescription("Activiteit", ListSortDirection.Ascending));
            }
        }

        private void ButtonVerwijderen_Click(object sender, RoutedEventArgs e)
        {
            if (ListBoxGekozen.SelectedIndex >= 0)
                ListBoxGekozen.Items.RemoveAt(ListBoxGekozen.SelectedIndex);
        }

        private void ButtonSamenvatting_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Wil je de gekozen hobby's op een rijtje?", "samenvatting", MessageBoxButton.YesNo, MessageBoxImage.Information, MessageBoxResult.No) == MessageBoxResult.Yes)
            {
                string mijnTekst = "mijn hobby's zijn: ";
                string cat = string.Empty;
                foreach (object item in ListBoxGekozen.Items)
                {
                    Hobby mijnHobby = (Hobby)item;
                    if (cat != mijnHobby.Categorie)
                    {
                        cat = mijnHobby.Categorie;
                        mijnTekst += "\n" + mijnHobby.Categorie + " : " + mijnHobby.Activiteit;
                    }
                    else
                        mijnTekst += ", " + mijnHobby.Activiteit;
                }
                if (ListBoxGekozen.Items.Count == 0)
                    MessageBox.Show("Ik heb geen hobby's", "samenvatting", MessageBoxButton.OK, MessageBoxImage.Information);
                else
                    MessageBox.Show(mijnTekst, "samenvatting", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}

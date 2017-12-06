using System;
using System.ComponentModel;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;

namespace ParkingBon
{
    /// <summary>
    /// Interaction logic for ParkingBonWindow.xaml
    /// </summary>
    public partial class ParkingBonWindow : Window
    {
        public ParkingBonWindow()
        {
            InitializeComponent();
            Nieuw();
        }


        private void Nieuw()
        { 
            DatumBon.SelectedDate = DateTime.Now;
            AankomstLabelTijd.Content = DateTime.Now.ToLongTimeString();
            TeBetalenLabel.Content = "0 €";
            VertrekLabelTijd.Content = AankomstLabelTijd.Content;
            StatusBarPath.Content = "Nieuwe bon";        
        }


        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (MessageBox.Show("Programma afsluiten ?", "Afsluiten", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No) == MessageBoxResult.No)
                e.Cancel = true;
        }

        private void minder_Click(object sender, RoutedEventArgs e)
        {
            int bedrag = Convert.ToInt32(TeBetalenLabel.Content.ToString().Replace(" €", ""));
            if (bedrag > 0)
                bedrag -= 1;
            if (bedrag == 0)
                Status_OpslaanEnAfdrukken();
            TeBetalenLabel.Content = bedrag.ToString() + " €";
            VertrekLabelTijd.Content = Convert.ToDateTime(AankomstLabelTijd.Content).AddHours(0.5 * bedrag).ToLongTimeString();
        }

        private void meer_Click(object sender, RoutedEventArgs e)
        {
            int bedrag = Convert.ToInt32(TeBetalenLabel.Content.ToString().Replace(" €", ""));
            DateTime vertrekuur = Convert.ToDateTime(AankomstLabelTijd.Content).AddHours(0.5 * bedrag);
            if (vertrekuur.Hour < 22)
                bedrag += 1;
            if (bedrag > 0)
                Status_OpslaanEnAfdrukken(true);
            TeBetalenLabel.Content = bedrag.ToString() + " €";
            VertrekLabelTijd.Content = Convert.ToDateTime(AankomstLabelTijd.Content).AddHours(0.5 * bedrag).ToLongTimeString();
        }
        private void Open_Executed(object sender,ExecutedRoutedEventArgs e)
        {
            try
            {
                OpenFileDialog dlg = new OpenFileDialog();
                dlg.FileName = "Bon";
                dlg.DefaultExt = ".bon";
                dlg.Filter = "Bonnen |*.bon";
                if (dlg.ShowDialog() == true)
                {
                    using (StreamReader bestand = new StreamReader(dlg.FileName))
                    {
                        TypeConverter convertDate = TypeDescriptor.GetConverter(typeof(DateTime));
                        DatumBon.SelectedDate = (DateTime)convertDate.ConvertFromString(bestand.ReadLine());
                        AankomstLabelTijd.Content = (DateTime)convertDate.ConvertFromString(bestand.ReadLine());
                        TeBetalenLabel.Content = (bestand.ReadLine());
                        VertrekLabelTijd.Content = (DateTime)convertDate.ConvertFromString(bestand.ReadLine());
                    }
                }
                StatusBarPath.Content = dlg.FileName.ToString();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Openen mislukt : "+ex.Message);
            }

        }
        private void Save_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                SaveFileDialog dlg = new SaveFileDialog();
                DateTime Datum = DatumBon.SelectedDate.Value;
                DateTime tijd = DateTime.Parse(AankomstLabelTijd.Content.ToString());
                dlg.FileName = $"{Datum.Day}-{Datum.Month}-{Datum.Year}om{tijd.Hour}-{tijd.Minute}-{tijd.Second}";
                dlg.DefaultExt = ".bon";
                dlg.Filter = "Bonnen |*.bon";
                if (dlg.ShowDialog() == true)
                {
                    using (StreamWriter bestand = new StreamWriter(dlg.FileName))
                    {
                        bestand.WriteLine(Datum.Date.ToShortDateString());
                        bestand.WriteLine(AankomstLabelTijd.Content.ToString());
                        bestand.WriteLine(TeBetalenLabel.Content.ToString());
                        bestand.WriteLine(VertrekLabelTijd.Content.ToString());
                    }
                }
                StatusBarPath.Content = dlg.FileName.ToString();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Opslaan mislukt : "+ex.Message);
            }
        }
        private void Print_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            AfdrukVoorbeeld afdruk = new AfdrukVoorbeeld();
            afdruk.AfdrukDocument = StelAfdrukSamen();
            afdruk.Show();
        }
        private void Close_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.Close();
        }
        public static RoutedCommand MijnRouteCtrlN = new RoutedCommand();
        private void CtrlN_Executed(object sender,ExecutedRoutedEventArgs e)
        {
            Nieuw();
        }
        private void Status_OpslaanEnAfdrukken(Boolean sw =false)
        {
            ToolButtonPrint.IsEnabled = sw;
            ToolButtonSave.IsEnabled = sw;
            MenuButtonSave.IsEnabled = sw;
            MenuButtonPrint.IsEnabled = sw;
        }

        private void NieuweBon_Click(object sender, RoutedEventArgs e)
        {
            Nieuw();
        }
        private double Breedte = 640;
        private double Hoogte = 416;
        private double vertPositie;
        private FixedDocument StelAfdrukSamen()
        {
            FixedDocument document = new FixedDocument();
            document.DocumentPaginator.PageSize = new Size(Breedte, Hoogte);
            PageContent inhoud = new PageContent();
            document.Pages.Add(inhoud);
            FixedPage page = new FixedPage();
            inhoud.Child = page;

            page.Width = Breedte;
            page.Height = Hoogte;
            vertPositie = 0;
            page.Margin = new Thickness(96,96,0,0);
            page.Children.Add(Image(@"Images/parkingbon.jpg"));
            page.Children.Add(Regel("Datum : " + DatumBon.Text.ToString()));
            page.Children.Add(Regel("Starttijd : " + AankomstLabelTijd.Content.ToString()));
            page.Children.Add(Regel("Eindtijd : " + VertrekLabelTijd.Content.ToString()));
            page.Children.Add(Regel("bedrag betaald : "+ TeBetalenLabel.Content.ToString()));        
            return document;
        }
        private TextBlock Regel(string tekst)
        {
            TextBlock deregel = new TextBlock();
            deregel.Text = tekst;
            deregel.FontSize = 18;
            deregel.Margin = new Thickness(204, vertPositie,0, 0);
            vertPositie += 30;
            return deregel;
        }
        private Image Image(string pad)
        {
            Image image = new Image();
            image.Source = logoImage.Source;
            image.Margin = new Thickness(0);
            return image;
        }
    }
}

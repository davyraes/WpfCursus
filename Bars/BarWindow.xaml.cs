using System;
using System.IO;
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
        private void Vet_aan_uit(Boolean wissel = false)
        {
            if ((wissel==true && TextBoxVoorbeeld.FontWeight==FontWeights.Bold)||(wissel==false&& TextBoxVoorbeeld.FontWeight ==FontWeights.Normal))
            {
                TextBoxVoorbeeld.FontWeight = FontWeights.Bold;
                MenuVet.IsChecked = true;
                StatusVet.FontWeight = FontWeights.Bold;
                ButtonVet.IsChecked = true;
            }
            else
            {
                TextBoxVoorbeeld.FontWeight = FontWeights.Normal;
                MenuVet.IsChecked = false;
                StatusVet.FontWeight = FontWeights.Normal;
                ButtonVet.IsChecked = false;
            }
        }
        private void Schuin_aan_uit(Boolean wissel = false)
        {
            
            if ((wissel==true&&TextBoxVoorbeeld.FontStyle == FontStyles.Italic)||(wissel == false&&TextBoxVoorbeeld.FontStyle==FontStyles.Normal))
            {
                TextBoxVoorbeeld.FontStyle = FontStyles.Italic;
                MenuSchuin.IsChecked = true;
                StatusSchuin.FontStyle = FontStyles.Italic;
                ButtonSchuin.IsChecked = true;
            }
            else
            {
                TextBoxVoorbeeld.FontStyle = FontStyles.Normal;
                MenuSchuin.IsChecked = false;
                StatusSchuin.FontStyle = FontStyles.Normal;
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
        private void SaveExecuted(object sender,ExecutedRoutedEventArgs e)
        {
            try
            {
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.FileName = "Document";
                dlg.DefaultExt = ".txt";
                dlg.Filter = "Text documents |*.txt";

                if (dlg.ShowDialog()==true)
                {
                    using (StreamWriter bestand = new StreamWriter(dlg.FileName))
                    {
                        bestand.WriteLine(LetterTypeComboBox.SelectedValue);
                        bestand.WriteLine(TextBoxVoorbeeld.FontWeight.ToString());
                        bestand.WriteLine(TextBoxVoorbeeld.FontStyle.ToString());
                        bestand.WriteLine(TextBoxVoorbeeld.Text);
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Opslaan mislukt : " + ex.Message);
            }
        }
        private void OpenExecuted(object sender,ExecutedRoutedEventArgs e)
        {
            try
            {
                OpenFileDialog dlg = new OpenFileDialog();
                dlg.FileName = "Document";
                dlg.DefaultExt = ".txt";
                dlg.Filter = "Text documents |*.txt";

                if(dlg.ShowDialog() == true)
                {
                    using (StreamReader bestand = new StreamReader(dlg.FileName))
                    {
                        LetterTypeComboBox.SelectedValue = new FontFamily(bestand.ReadLine());
                        TypeConverter convertBold = TypeDescriptor.GetConverter(typeof(FontWeight));
                        TextBoxVoorbeeld.FontWeight = (FontWeight)convertBold.ConvertFromString(bestand.ReadLine());
                        Vet_aan_uit(true);
                        TypeConverter ConvertStyle = TypeDescriptor.GetConverter(typeof(FontStyle));
                        TextBoxVoorbeeld.FontStyle = (FontStyle)ConvertStyle.ConvertFromString(bestand.ReadLine());
                        Schuin_aan_uit(true);
                        TextBoxVoorbeeld.Text = bestand.ReadLine();
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Openen mislukt : " + ex.Message);
            }
        }
        private void PrintExecuted(object sender,ExecutedRoutedEventArgs e)
        {
            PrintDialog afdrukken = new PrintDialog();
            if (afdrukken.ShowDialog() == true)
                afdrukken.PrintDocument(StelAfdrukSamen().DocumentPaginator, "tekstbox");
        }
        private double A4Breedte = 21 / 2.54 * 96;
        private double A4Hoogte = 29.7 / 2.54 * 96;
        private double vertPositie;
        private FixedDocument StelAfdrukSamen()
        {
            FixedDocument document = new FixedDocument();
            document.DocumentPaginator.PageSize = new Size(A4Breedte, A4Hoogte);
            PageContent inhoud = new PageContent();
            document.Pages.Add(inhoud);
            FixedPage page = new FixedPage();
            inhoud.Child = page;

            page.Width = A4Breedte;
            page.Height = A4Hoogte;
            vertPositie = 96;

            page.Children.Add(Regel("gebruikt lettertype : " + TextBoxVoorbeeld.FontFamily.ToString()));
            page.Children.Add(Regel("gewicht van het lettertype : " + TextBoxVoorbeeld.FontWeight.ToString()));
            page.Children.Add(Regel("Stijl van het lettertype : " + TextBoxVoorbeeld.FontStyle.ToString()));
            page.Children.Add(Regel(""));
            page.Children.Add(Regel("Inhoud van de tekstbox : "));
            for (int i = 0; i < TextBoxVoorbeeld.LineCount; i++)
            {
                page.Children.Add(Regel(TextBoxVoorbeeld.GetLineText(i)));
            }
            return document;
        }
        private TextBlock Regel(string tekst)
        {
            TextBlock deregel = new TextBlock();
            deregel.Text = tekst;
            deregel.FontSize = TextBoxVoorbeeld.FontSize;
            deregel.FontWeight = TextBoxVoorbeeld.FontWeight;
            deregel.FontStyle = TextBoxVoorbeeld.FontStyle;
            deregel.Margin = new Thickness(96, vertPositie, 96, 96);
            vertPositie += 30;
            return deregel;
        }
        private void PreviewExecuted(object sender,ExecutedRoutedEventArgs e)
        {
            Afdrukvoorbeeld preview = new Afdrukvoorbeeld();
            preview.Owner = this;
            preview.AfdrukDocument = StelAfdrukSamen();
            preview.ShowDialog();
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            if (MessageBox.Show("programma afsluiten ?", "Afsluiten", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No) == MessageBoxResult.No)
                e.Cancel = true;
        }
        private void CloseExecuted(object sender,ExecutedRoutedEventArgs e)
        {
            this.Close();
        }
    }
}

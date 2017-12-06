using System;
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
using System.Reflection;

namespace BloemSamenstelling
{
    /// <summary>
    /// Interaction logic for BloemWindow.xaml
    /// </summary>
    public partial class BloemWindow : Window
    {
         public BloemWindow()
        {
            InitializeComponent();
            foreach(PropertyInfo info in typeof(Colors).GetProperties())
            {
                BrushConverter bc = new BrushConverter();
                SolidColorBrush dekleur = (SolidColorBrush)bc.ConvertFromString(info.Name);
                Kleur kleur = new Kleur();
                kleur.Naam = info.Name;
                kleur.Borstel = dekleur;
                kleur.Hex = dekleur.ToString();
                kleur.Rood = dekleur.Color.R;
                kleur.Groen = dekleur.Color.G;
                kleur.Blauw = dekleur.Color.B;
                cirkelsKleuren.Items.Add(kleur);
                cirkelKaderKleuren.Items.Add(kleur);
                rechthoekenKleuren.Items.Add(kleur);
                rechthoekKaderKleuren.Items.Add(kleur);
            }
        }
    }
}

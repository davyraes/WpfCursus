using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Wenskaart.Model
{
    public class Wenskaart
    {
        public Wenskaart()
        {
            Afbeelding = new ImageBrush();
            Wens = string.Empty;
            TekstGrootte = 20;
            Font = new FontFamily("Arial");
            Bollen = new List<Bol>();
            Pad = "Nieuw";
        }
        public ImageBrush Afbeelding { get; set; }
        public string Wens { get; set; }
        public int TekstGrootte { get; set; }
        public FontFamily Font { get; set; }
        public List<Bol> Bollen { get; set; }
        public string Pad { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Wenskaart.Model
{
    public class Bol
    {
        public Bol(double nX,double nY,SolidColorBrush nKleur)
        {
            Xpositie = nX;
            Ypositie = nY;
            Kleur = nKleur;
        }
        public double Xpositie { get; set; }
        public double Ypositie { get; set; }
        public SolidColorBrush Kleur { get; set; }
    }
}

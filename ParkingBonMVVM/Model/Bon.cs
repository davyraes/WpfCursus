using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingBonMVVM.Model
{
    public class Bon
    {
        public Bon()
        {
            Datum = DateTime.Now;
            BeginTijd = DateTime.Now;
            EindTijd = BeginTijd;
            Bedrag = 0;
        }
        public DateTime Datum { get; set; }
        public DateTime BeginTijd { get; set; }
        public int Bedrag { get; set; }
        public DateTime EindTijd { get; set; }
    }
}

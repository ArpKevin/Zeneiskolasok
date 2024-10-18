using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zeneiskolasok
{
    internal class Zenedarab
    {
        public string Cim { get; set; }
        public int KeletkezesiEv { get; set; }
        public int Nehezseg { get; set; }
        public string Szerzo { get; set; }

        public Zenedarab(string sor)
        {
            var x = sor.Split(";");
            Cim = x[0];
            KeletkezesiEv = int.Parse(x[1]);
            Nehezseg = int.Parse(x[2]);
            Szerzo = x[3];
        }

        public override string ToString()
        {
            return $"Cím: {Cim}, Keletkezési év: {KeletkezesiEv}, Nehézség: {Nehezseg}, Szerző: {Szerzo}";
        }
    }
}

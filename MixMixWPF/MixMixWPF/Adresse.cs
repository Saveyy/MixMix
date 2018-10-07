using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MixMixWPF
{
    public class Adresse
    {
        public string adressebetegnelse { get; set; }
        public Adgangsadresse adgangsadresse { get; set; }
        public string dør { get; set; }
        public string etage { get; set; }

        public override string ToString()
        {
            return adressebetegnelse;
        }

    }
}

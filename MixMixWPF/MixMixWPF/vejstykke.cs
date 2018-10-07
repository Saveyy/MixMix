using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MixMixWPF
{
    public class Vejstykke
    {
        public string navn { get; set; }

        public override string ToString()
        {
            return navn;
        }
    }
}

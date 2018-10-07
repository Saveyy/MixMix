using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entities
{
    public class Measurement
    {
        public Measurement()
        {

        }

        public Measurement(string type, double proportion)
        {
            Type = type;
            Proportion = proportion;
        }

        public int Id { get; set; }
        public string Type { get; set; }
        public double Proportion { get; set; }
        
    }
}
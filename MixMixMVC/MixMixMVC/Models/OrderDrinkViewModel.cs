using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MixMixMVC.Models
{
    public class OrderDrinkViewModel
    {
        public int DrinkId { get; set; }
        public int BarId { get; set; }
        public string Ingredients { get; set; }
        public List<string> Names { get; set; }
        public double Subtotal { get; set; }
        public int Quantity { get; set; }
    }
}
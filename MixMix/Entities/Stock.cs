using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entities
{
    public class Stock
    {
        public Stock()
        {

        }
        public Ingredient Ingredient { get; set; }
        public double Quantity { get; set; }
    }
}
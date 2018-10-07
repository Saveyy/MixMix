using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entities
{
    public class Price
    {
        public Price()
        {

        }
        public Ingredient Ingredient { get; set; }
        public double SellingPrice { get; set; }
        public double BuyingPrice { get; set; }
    }
}
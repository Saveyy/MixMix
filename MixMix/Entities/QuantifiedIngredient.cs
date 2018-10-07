using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entities
{
    public class QuantifiedIngredient
    {
        public QuantifiedIngredient()
        {

        }

        public QuantifiedIngredient(double quantity, Ingredient ingredient, Measurement measurement)
        {
            Quantity = quantity;
            Ingredient = ingredient;
            Measurement = measurement;
        }
        public int Id { get; set; }
        public double Quantity { get; set; }
        public Ingredient Ingredient { get; set; }
        public Measurement Measurement { get; set; }
        
    }
}
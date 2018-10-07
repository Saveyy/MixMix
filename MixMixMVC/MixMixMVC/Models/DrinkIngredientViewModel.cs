using MixMixMVC.DrinkServiceReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MixMixMVC.Models
{
    public class DrinkIngredientViewModel
    {
        public string Ingredient { get; set; }
        public string Measurement { get; set; }
        public double Quantity { get; set; }
        public List<Ingredient> Ingredients { get; set; }
        public List<Measurement> Measurements { get; set; }
        public int BarId { get; set; }
        public string DrinkName { get; set; }
        public List<QuantifiedIngredient> QuantifiedIngredients { get; set; }
    }
}
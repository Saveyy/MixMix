using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class DrinkViewModel
    {
        public DrinkViewModel()
        {

        }
        public DrinkViewModel(int id, string ingredients)
        {
            this.Id = id;
            this.Ingredients = ingredients;
            Names = new List<string>();
        }
        public int Id { get; set; }
        public string PopName { get; set; }
        public List<string> Names { get; set; }
        public string Ingredients { get; set; }
        public double Price { get; set; }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}

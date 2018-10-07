using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Entities
{
    public class Drink : IEquatable<Drink>
    {
        public Drink()
        {

        }

        public Drink(List<string> names, List<QuantifiedIngredient> ingredients, string recipe)
        {
            this.Names = new List<string>();
            this.Names = names;
            this.Ingredients = ingredients;
            this.Recipe = recipe;
        }

        public int Id { get; set; }
        public List<string> Names { get; set; }
        public List<QuantifiedIngredient> Ingredients { get; set; }
        public string Recipe { get; set; }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public bool Equals(Drink other)
        {
            if (this.Id == other.Id)
            {
                return true;
            }
            return false;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (ReferenceEquals(obj, this))
            {
                return true;
            }
            if (obj.GetType() != this.GetType())
            {
                return false;
            }
            return this.Equals(obj as Drink);
        }
        public static bool operator ==(Drink lhs, Drink rhs)
        {
            return object.Equals(lhs, rhs);
        }
        public static bool operator !=(Drink lhs, Drink rhs)
        {
            return !object.Equals(lhs, rhs);
        }
    }
}
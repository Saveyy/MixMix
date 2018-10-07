using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entities
{
    public class Ingredient : IEquatable<Ingredient>
    {
        public Ingredient()
        {

        }

        public Ingredient(string name, bool measurable, double alch)
        {
            Name = name;
            Measurable = measurable;
            Alch = alch;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public bool Measurable { get; set; }
        public double Alch { get; set; }

        public bool Equals(Ingredient other)
        {
            if (this.Id == other.Id &&
                this.Name.Equals(other.Name) &&
                this.Measurable.Equals(other.Measurable) &&
                this.Alch.Equals(other.Alch))
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
            return this.Equals(obj as Ingredient);
        }

        public bool Equals(Ingredient x, Ingredient y)
        {
            return x.Equals(y);
        }
        
        public override int GetHashCode()
        {
            return Id.GetHashCode() ^ Name.GetHashCode() ^ Measurable.GetHashCode() ^ Alch.GetHashCode();
        }

        public static bool operator ==(Ingredient lhs, Ingredient rhs)
        {
            return object.Equals(lhs, rhs);
        }
        public static bool operator !=(Ingredient lhs, Ingredient rhs)
        {
            return !object.Equals(lhs, rhs);
        }
        
    }
}
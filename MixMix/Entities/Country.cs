using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Country : IEquatable<Country>
    {
        public int Id { get; set; }
        [DataMember]
        public string CountryName { get; set; }

        public Country(int id, string countryName)
        {
            Id = id;
            CountryName = countryName;
        }

        public Country(string countryName)
        {
            CountryName = countryName;
        }
        public Country()
        {

        }

        public override int GetHashCode()
        {
            return Id.GetHashCode() ^ CountryName.GetHashCode();
        }

        public bool Equals(Country other)
        {
            if (this.Id == other.Id &&
                this.CountryName.Equals(other.CountryName))
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
            return this.Equals(obj as Country);
        }
        public static bool operator ==(Country lhs, Country rhs)
        {
            return object.Equals(lhs, rhs);
        }
        public static bool operator !=(Country lhs, Country rhs)
        {
            return !object.Equals(lhs, rhs);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Zipcode : IEquatable<Zipcode>
    {
        public int Id { get; set; }
        public string Zip { get; set; }
        public string CityName { get; set; }

        public Country Country { get; set; }

        public Zipcode(int id, string zip, string cityName, Country country)
        {
            Id = id;
            Zip = zip;
            CityName = cityName;
            Country = country;
        }

        public Zipcode(string zip, string cityName, Country country)
        {
            Zip = zip;
            CityName = cityName;
            Country = country;
        }
        public Zipcode()
        {

        }

        public override int GetHashCode()
        {
            return Id.GetHashCode() ^ Country.GetHashCode() ^ CityName.GetHashCode() ^ Zip.GetHashCode();
        }

        public bool Equals(Zipcode other)
        {
            if (this.CityName.Equals(other.CityName) &&
                this.Country.Equals(other.Country) &&
                this.Id == other.Id &&
                this.Zip.Equals(other.Zip))
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
            return this.Equals(obj as Zipcode);
        }
        public static bool operator ==(Zipcode lhs, Zipcode rhs)
        {
            return object.Equals(lhs, rhs);
        }
        public static bool operator !=(Zipcode lhs, Zipcode rhs)
        {
            return !object.Equals(lhs, rhs);
        }
    }
}

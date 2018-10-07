using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Address : IEquatable<Address>
    {
        public Address()
        {

        }
        public Address(string addressName)
        {
            AddressName = addressName;
        }

        public Address(string addressName, Zipcode zip)
        {
            AddressName = addressName;
            Zipcode = zip;
        }

        public Address(int id, string addressName, Zipcode zip)
        {
            Id = id;
            AddressName = addressName;
            Zipcode = zip;
        }

        public int Id { get; set; }
        [DataMember]
        public string AddressName { get; set; }
        public Zipcode Zipcode { get; set; }

        public override int GetHashCode()
        {
            return Id.GetHashCode() ^ AddressName.GetHashCode() ^ Zipcode.GetHashCode();
        }

        public bool Equals(Address other)
        {
            if (this.Id == other.Id &&
                this.AddressName.Equals(other.AddressName) &&
                this.Zipcode.Equals(other.Zipcode))
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
            return this.Equals(obj as Address);
        }
        public static bool operator ==(Address lhs, Address rhs)
        {
            return object.Equals(lhs, rhs);
        }
        public static bool operator !=(Address lhs, Address rhs)
        {
            return !object.Equals(lhs, rhs);
        }

    }
}

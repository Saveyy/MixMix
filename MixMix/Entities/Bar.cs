using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;
using System.Runtime.Serialization;

namespace Entities
{
    [DataContract]
    public class Bar : Contact, IEquatable<Bar>
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public Address Address { get; set; }

        public List<Drink> Specialities { get; set; }
        public List<Stock> Stocks { get; set; }
        public List<Price> Prices { get; set; }


        public Bar(string name, Address address, string phonenumber, string email, string username)
        {
            Name = name;
            Address = address;
            PhoneNumber = phonenumber;
            Email = email;
            Username = username;
        }

        public Bar(string name, Address address, string phonenumber, string email, string username, int id)
        {
            Name = name;
            Address = address;
            PhoneNumber = phonenumber;
            Email = email;
            Username = username;
            ID = id;
        }

        public Bar()
        {

        }

        public bool Equals(Bar other)
        {
            if (this.ID == other.ID &&
                this.Address.Equals(other.Address) &&
                this.Email.Equals(other.Email) &&
                this.Name.Equals(other.Name) &&
                this.PhoneNumber.Equals(other.PhoneNumber) &&
                this.Username.Equals(other.Username)
                )
            {
                return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode() ^ PhoneNumber.GetHashCode() ^ Email.GetHashCode() ^ Username.GetHashCode() ^ ID.GetHashCode() ^ Address.GetHashCode();
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
            return this.Equals(obj as Bar);
        }

        public static bool operator ==(Bar lhs, Bar rhs)
        {
            return object.Equals(lhs, rhs);
        }
        public static bool operator !=(Bar lhs, Bar rhs)
        {
            return !object.Equals(lhs, rhs);
        }


        //public Manager Manager { get; set; }
        //public List<Event> Events { get; set; }
        //public List<Order> Orders { get; set; }
        
    }
}
 
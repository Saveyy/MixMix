using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entities
{
    public class Manager : Contact, IEquatable<Manager>
    {
        public Manager(string name, string phoneNumber, string email, string username, int? id)
        {
            this.Name = name;
            this.PhoneNumber = phoneNumber;
            this.Email = email;
            this.Username = username;
            this.Id = id ?? -1;
        }

        public Manager() { }


        public int Id { get; set; }
        public List<Bar> Bars { get; set; }


        
        public override int GetHashCode()
        {
            return Name.GetHashCode() ^ PhoneNumber.GetHashCode() ^ Email.GetHashCode() ^ Username.GetHashCode() ^ Id.GetHashCode();
        }

        public bool Equals(Manager other)
        {
            if (this.Id == other.Id &&
                this.Name.Equals(other.Name) &&
                this.PhoneNumber.Equals(other.PhoneNumber) &&
                this.Username.Equals(other.Username) &&
                this.Email.Equals(other.Email))
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
            return this.Equals(obj as Manager);
        }

        public static bool operator ==(Manager lhs, Manager rhs)
        {
            return object.Equals(lhs, rhs);
        }
        public static bool operator !=(Manager lhs, Manager rhs)
        {
            return !object.Equals(lhs, rhs);
        }
    }
    public enum ManagerColumn
    {
        name,
        phonenumber,
        email,
        username,
        hashedPassword,
        salt
    }
}
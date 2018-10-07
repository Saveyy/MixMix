using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataBase;
using Entities;

namespace Controller
{
    public class LocationController
    {

        #region new methods
        public Address CreateAddress(Address address)
        {
            return locationDB.CreateAddress(address);
        }
        
        public bool Update(Address address)
        {
            return locationDB.UpdateAddress(address);
        }
        #endregion

        public bool Delete(int id)
        {
            return locationDB.Delete(id);
        }

        public List<Country> getCountries()
        {
            return locationDB.getCountries();
        }

        public List<Zipcode> getZipcodesByCountryId(int countryId)
        {
            return locationDB.getZipcodesByCountryId(countryId);
        }



        #region old methods
        //Vi antager at UI har et ID på Country fra databasen og sender det med
        LocationDB locationDB = LocationDB.Instance;
        public Address CreateAddress(string addressName, int zipcode)
        {
            Address address = new Address(addressName);
            //Tilføj fejl håndtering
            return locationDB.CreateAddress(address, zipcode);
        }

        public Address Update(Address address, int zipcodeId)
        {
            //Tilføj fejl håndtering
            return locationDB.UpdateAddress(address, zipcodeId);
        }


        #endregion
    }
}

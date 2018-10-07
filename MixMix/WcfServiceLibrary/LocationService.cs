using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Controller;
using Entities;
using WcfServiceLibrary.ServiceInterfaces;

namespace WcfServiceLibrary
{
    public class LocationService : ILocationService
    {
        private LocationController locationController = new LocationController();

        public Address createAddress(string addressName, int zipcode)
        {
            return locationController.CreateAddress(addressName, zipcode);
        }

        public bool deleteAddress(int id)
        {
            return locationController.Delete(id);
        }

        public List<Country> getCountries()
        {
            return locationController.getCountries();
        }

        public List<Zipcode> getZipcodesById(int countryId)
        {
            return locationController.getZipcodesByCountryId(countryId);
        }

        public Address updateAddress(Address address, int zipcodeId)
        {
            return locationController.Update(address, zipcodeId);
        }
    }
}

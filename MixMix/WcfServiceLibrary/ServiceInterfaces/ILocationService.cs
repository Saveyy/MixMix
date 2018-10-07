using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Entities;

namespace WcfServiceLibrary.ServiceInterfaces
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface ILocationService
    {
        [OperationContract]
        List<Country> getCountries();

        [OperationContract]
        List<Zipcode> getZipcodesById(int countryId);

        [OperationContract]
        Address createAddress(string addressName, int zipcode);

        [OperationContract]
        Address updateAddress(Address address, int zipcodeId);

        [OperationContract]
        bool deleteAddress(int id);
    }

}

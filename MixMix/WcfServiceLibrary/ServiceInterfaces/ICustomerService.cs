using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfServiceLibrary.ServiceInterfaces
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IICustomerService" in both code and config file together.
    [ServiceContract]
    public interface ICustomerService
    {
        [OperationContract]
        Customer FindCustomer(int id);
        [OperationContract]
        Customer FindCustomerByUsername(string username);

        [OperationContract]
        IEnumerable<Customer> FindCustomers(CustomerColumn columnName, string search);

        [OperationContract]
        bool DeleteCustomer(int id);

        [OperationContract]
        bool UpdateCustomer(Customer customer);

        [OperationContract]
        Customer LoginCustomer(string username, string password);

    }
}

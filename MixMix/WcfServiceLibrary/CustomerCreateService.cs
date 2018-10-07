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
    public class CustomerCreateService : ICreateCustomerService
    {
        CustomerController controller = new CustomerController();

        public Customer CreateCustomer(Customer customer, string password)
        {
            return controller.Create(customer, password);
        }
    }
}

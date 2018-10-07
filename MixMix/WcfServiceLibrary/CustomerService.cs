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
    public class CustomerService : ICustomerService
    {
        CustomerController customerController { get; set; } = new CustomerController();


        public bool DeleteCustomer(int id)
        {
            return customerController.Delete(id);
        }

        public Customer FindCustomer(int id)
        {
            return customerController.Find(id);
        }

        public Customer FindCustomerByUsername(string username)
        {
            return customerController.Find(username);
        }

        public IEnumerable<Customer> FindCustomers(CustomerColumn columnName, string search)
        {
            return customerController.Find(columnName, search);
        }

        public bool UpdateCustomer(Customer customer)
        {
            return customerController.Update(customer);
        }

        public Customer LoginCustomer(string username, string password)
        {
            return customerController.Login(username, password);
        }
    }
}

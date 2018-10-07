using DataBase;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controller
{
    public class CustomerController : ILoginAble<Customer>
    {
        CustomerDB CustomerDB;

        public CustomerController()
        {
            CustomerDB = new CustomerDB();
        }
        
        public Customer Create(Customer customer, string password)
        {
            return CustomerDB.Insert(customer, password);
        }
        public Customer Find(int id)
        {
            return CustomerDB.Find(id);
        }
        public Customer Find(string username)
        {
            return CustomerDB.Find(username);
        }
        public List<Customer> Find(CustomerColumn columnName, string search)
        {
            return CustomerDB.Find(columnName, search);
        }
        public bool Update(Customer customer)
        {
            return CustomerDB.Update(customer);
        }
        public bool Delete(int id)
        {
            return CustomerDB.Delete(id);
        }

        public Customer Login(string username, string password)
        {
            return CustomerDB.Login(username, password);
        }

    }
}

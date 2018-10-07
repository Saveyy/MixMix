using Controller;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using WcfServiceLibrary.ServiceInterfaces;

namespace WcfServiceLibrary
{
    public class AuthService : IAuthService
    {
        public bool Login(string username, string password)
        {
            ILoginAble<Customer> controller = new CustomerController();
            Customer obj = controller.Login(username, password);
            return obj != null;
        }
    }
}

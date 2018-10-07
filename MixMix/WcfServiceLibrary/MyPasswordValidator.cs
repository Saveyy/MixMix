using Controller;
using Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Selectors;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;


namespace WcfServiceLibrary
{
    public class MyPasswordValidator : UserNamePasswordValidator
    {
        public override void Validate(string userName, string password)
        {
            bool isSuccess = false;
            //Doesn't actually need to check - As they are already checked in AuthService - 

            ILoginAble<Customer> controller = new CustomerController();
            Customer obj = controller.Login(userName, password);
            isSuccess = obj != null;

            if (!isSuccess)
            {
                throw new FaultException<Exception>(new Exception("Incorrect Login"), "Invalid login Attemp");
            }
        }
    }
}
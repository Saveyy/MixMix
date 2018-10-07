using Controller;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WcfServiceLibrary.ServiceInterfaces;

namespace WcfServiceLibrary
{
    public class ManagerService : IManagerService
    {
        private ManagerController CTRLmanager = new ManagerController();

        public Manager CreateManager(string name)
        {
            throw new NotImplementedException();
        }

        public Manager Login(string username, string password)
        {
            return CTRLmanager.Login(username, password);
        }
    }
}

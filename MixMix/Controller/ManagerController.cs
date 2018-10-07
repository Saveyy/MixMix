using DataBase;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Entities.Manager;

namespace Controller
{
    public class ManagerController : ILoginAble<Manager>
    {
        ManagerDB managerDB = new ManagerDB();
        public Manager CreateManager(Manager manager, string password)
        {
            return managerDB.Insert(manager, password);
        }

        public Manager FindManager(int id)
        {
            return managerDB.Find(id);
        }
        public List<Manager> FindManager(ManagerColumn columnName, string search)
        {
            return managerDB.Find(columnName, search);
        }
        public bool UpdateManager(Manager manager)
        {
            return managerDB.Update(manager);
        }
        public bool DeleteManager(int id)
        {
            return managerDB.Delete(id);
        }

        public Manager Login(string username, string password)
        {
            return managerDB.Login(username, password);
        }

    }
}

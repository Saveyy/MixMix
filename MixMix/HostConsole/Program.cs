using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using WcfServiceLibrary;
using WcfServiceLibrary.ServiceInterfaces;

namespace HostConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceHost AuthService = new ServiceHost(typeof(AuthService));
            AuthService.Open();

            ServiceHost drinkService = new ServiceHost(typeof(DrinkService));
            drinkService.Open();

            ServiceHost barService = new ServiceHost(typeof(BarService));
            barService.Open();

            ServiceHost locationService = new ServiceHost(typeof(LocationService));
            locationService.Open();

            ServiceHost customerService = new ServiceHost(typeof(CustomerService));
            customerService.Open();

            ServiceHost managerService = new ServiceHost(typeof(ManagerService));
            managerService.Open();

            ServiceHost orderService = new ServiceHost(typeof(OrderService));
            orderService.Open();

            ServiceHost customerCreateService = new ServiceHost(typeof(CustomerCreateService));
            customerCreateService.Open();

            Console.WriteLine("Press any key to exit");
            Console.ReadLine();

            customerCreateService.Close();
            locationService.Close();
            customerService.Close();
            managerService.Close();
            orderService.Close();
            drinkService.Close();
            AuthService.Close();
            barService.Close();
        }
    }
}

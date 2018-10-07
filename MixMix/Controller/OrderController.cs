using DataBase;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controller
{
    public class OrderController
    {
        OrderDB orderDB = new OrderDB();
        BarController barController = new BarController();
        public int CreateOrder(Order order)
        {
            return orderDB.CreateOrder(order);
        }

        public bool DecrementStock(int drinkId, int barId)
        {
            return barController.DecrementStock(drinkId, barId);
        }
    }
}

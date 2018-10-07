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
    public class OrderService : IOrderService
    {
        OrderController orderController = new OrderController();
        public int CreateOrder(Order order)
        {
            return orderController.CreateOrder(order);
        }

        public bool DecrementStock(int drinkId, int barId)
        {
            return orderController.DecrementStock(drinkId, barId);
        }
    }
}

using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace WcfServiceLibrary.ServiceInterfaces
{
    [ServiceContract]
    interface IOrderService
    {
        [OperationContract]
        int CreateOrder(Order order);
        [OperationContract]
        bool DecrementStock(int drinkId, int barId);
    }
}

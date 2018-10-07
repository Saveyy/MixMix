using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entities
{
    public class Order
    {
        public Order()
        {

        }

        public int Id { get; set; }
        public int OrderNumber { get; set; }
        public List<OrderLine> OrderLines { get; set; }
        public DateTime OrderTime { get; set; }
        public DateTime DeliveryTime { get; set; }
        public Bar Bar { get; set; }
        public Status Status { get; set; }
        public Customer Customer { get; set; }
    }

    public enum Status
    {
        NotDone,
        Done,
        Delivered
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Entities
{
    [DataContract]
    public class OrderLine
    {
        public OrderLine()
        {

        }
        [DataMember]
        public Drink Drink { get; set; }
        [DataMember]
        public List<string> Names { get; set; }
        [DataMember]
        public int Quantity { get; set; }
        [DataMember]
        public double Subtotal { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Text;

namespace EventGridCustomTopic
{
    internal class Order
    {
        public string OrderId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }  
    }
}

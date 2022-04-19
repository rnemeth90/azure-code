using System;
using System.Collections.Generic;
using System.Text;

namespace AzureEventGridCustomTopic
{
    internal class Order
    {
        public string OrderId { get; set; }
        public int Quantity { get; set; }
        public decimal? UnitPrice { get; set; }
    }
}

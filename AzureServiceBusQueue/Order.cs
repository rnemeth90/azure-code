using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace AzureServiceBusQueue
{
    internal class Order
    {
        public int OrderId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}

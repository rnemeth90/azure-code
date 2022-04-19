using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace AzureEventHubApp
{
    internal class Order
    {
        //private static ILogger _logger;
        
        public string MessageId { get; set; }
        public int OrderId { get; set; }
        public string Name { get; set; }
        public double UnitPrice { get; set; }
        public int Quantity { get; set; }

        //public Order(ILogger<Order> logger)
        //{
        //    _logger = logger;
        //}

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}

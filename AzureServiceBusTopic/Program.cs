using System;

namespace AzureServiceBusTopic
{
    internal class Program
    {
        private static string _connectionString = @"Endpoint=sb://azrtnsb02.servicebus.windows.net/;SharedAccessKeyName=manage;SharedAccessKey=GtAWU5UBXO2aQsyF2B0RFVNNmyHuCttkiKWVI3WoUL4=;EntityPath=apptopic01";
        private static string _queueName = "apptopic01";
        static void Main(string[] args)
        {
            List<Order> orders = new List<Order>()
            {
                new Order() { OrderId=1, Quantity=1, UnitPrice= 9.99M},
                new Order() { OrderId=2, Quantity=1, UnitPrice= 9.99M},
                new Order() { OrderId=3, Quantity=1, UnitPrice= 9.99M},
                new Order() { OrderId=4, Quantity=1, UnitPrice= 9.99M},
                new Order() { OrderId=5, Quantity=1, UnitPrice= 9.99M},
                new Order() { OrderId=6, Quantity=1, UnitPrice= 9.99M}
            };
        }
    }
}

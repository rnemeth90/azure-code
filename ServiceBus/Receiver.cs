using Azure.Messaging.ServiceBus;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceBusApp
{
    public class Receiver
    {
        
        private ServiceBusClient _serviceBusClient;
        private ServiceBusReceiver _serviceBusReceiver;
        public ServiceBusClient ServiceBusClient { get => _serviceBusClient; set => _serviceBusClient = value; }
    }
}

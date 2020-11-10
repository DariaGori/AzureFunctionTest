using Domain;
using Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.ServiceBus.Core;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Text;

namespace FunctionApp
{
    public static class ServiceBusMessageReceiver
    {
        const string QueueName = "myqueue";

        [FunctionName("ServiceBusMessageReceiver")]
        public static async void ServiceBusOutput([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req, ILogger log)
        {
            log.LogInformation("HTTP trigger function processed a request.");

            var messageReceiver = new MessageReceiver(Environment.GetEnvironmentVariable("BusConnection"), QueueName, 
                ReceiveMode.ReceiveAndDelete, null, 1);

            var tempMessage = await messageReceiver.ReceiveAsync(TimeSpan.FromSeconds(1));

            if (tempMessage == null || tempMessage.Body == null)
            {
                log.LogInformation("There are no messages currently in the queue");
            } else
            {
                var payload = BinarySerializer.Deserialize<QueueMessage>(tempMessage.Body);

                log.LogInformation($"HTTP trigger function processed message: {payload.OrderNumber} - {payload.OrderDescription}");
            }
        }
    }
}

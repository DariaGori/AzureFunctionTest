using System;
using System.IO;
using Domain;
using Helper;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace FunctionApp
{
    public static class ServiceBusQueueTrigger
    {
        const string QueueName = "myqueue";

        // Push-based processing
        [FunctionName("ServiceBusQueueTrigger")]
        public static void Run(
           [ServiceBusTrigger(QueueName, Connection = "BusConnection")] Message myQueueItem,
            Int32 deliveryCount,
            DateTime enqueuedTimeUtc,
            string messageId,
            ILogger log)
        {
            // Getting data from payload
            var queueItem = BinarySerializer.Deserialize<QueueMessage>(myQueueItem.Body);
            log.LogInformation($"The order number: {queueItem.OrderNumber}, order description: {queueItem.OrderDescription}");

            // Getting metadata directly
            log.LogInformation($"EnqueuedTimeUtc={enqueuedTimeUtc}");
            log.LogInformation($"DeliveryCount={deliveryCount}");
            log.LogInformation($"MessageId={messageId}");

            // Getting metadata via Message
            log.LogInformation($"Order to {myQueueItem.Label}");
        }
    }
}

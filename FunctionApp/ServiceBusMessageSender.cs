using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.ServiceBus;
using System.Configuration;
using Domain;
using Helper;
using System;

namespace FunctionApp
{
    public static class ServiceBusMessageSender
    {
        const string QueueName = "myqueue";
        static IQueueClient _queueClient;

        // Preprocessor for messages before entering the queue
        // req parameter is a placeholder for the actual request we'll be receiving
        [FunctionName("ServiceBusMessageSender")]
        [return: ServiceBus(QueueName, Connection = "BusConnection")]
        public static async Task Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            _queueClient = new QueueClient(
                Environment.GetEnvironmentVariable("BusConnection", EnvironmentVariableTarget.Process), QueueName);

            var messageObject = new QueueMessage
            {
                OrderNumber = 1,
                OrderDescription = "Test"
            };

            var message = new Message()
            {
                Label = "Tesco",
                Body = BinarySerializer.Serialize(messageObject)
            };

            await _queueClient.SendAsync(message);
            await _queueClient.CloseAsync();
        }
    }
}

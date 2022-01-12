using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Text;

namespace Report.Api.MQServices
{
    public class RabbitMQPublisher
    {
        private readonly RabbitMQClientService _rabbitMQClientService;

        public RabbitMQPublisher(RabbitMQClientService rabbitMQClientService)
        {
            _rabbitMQClientService = rabbitMQClientService;
        }

        internal void Publish(Domain.Report reportDocumentCreatedEvent)
        {
            var channel = _rabbitMQClientService.Connect();
            var messageArr = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(reportDocumentCreatedEvent));
            var properties = channel.CreateBasicProperties();
            properties.Persistent = true;
            channel.BasicPublish(RabbitMQClientService.DocumentCreateExchange, RabbitMQClientService.RoutingDocument, properties, messageArr);

            Console.WriteLine($"Message Published For Create Excel Report {reportDocumentCreatedEvent.DocumentStatus}");
        }
    }
}

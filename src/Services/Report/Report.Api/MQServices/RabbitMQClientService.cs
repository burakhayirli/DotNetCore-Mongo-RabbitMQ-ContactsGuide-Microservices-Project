using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Report.Api.MQServices
{
    public class RabbitMQClientService:IDisposable
    {
        private readonly ConnectionFactory _connectionFactory;
        private IConnection _connection;
        private IModel _channel;
        IModel channel => _channel ?? (_channel = GetChannel());
        public static string CreateDocumentQueueName = "create_document_queue";
        public static string DocumentCreateExchange = "document_create_exchange";
        public static string RoutingDocument = "document_route";

        private readonly ILogger<RabbitMQClientService> _logger;
        public RabbitMQClientService(ConnectionFactory connectionFactory, ILogger<RabbitMQClientService> logger)
        {
            _connectionFactory = connectionFactory;
            _logger = logger;
        }
        public IModel Connect()
        {
            if (_connection == null || !_connection.IsOpen)
            {
                _connection = _connectionFactory.CreateConnection();
                _logger.LogInformation("Connected to RabbitMQ");
            }
            channel.ExchangeDeclare(DocumentCreateExchange, type: ExchangeType.Direct, durable: true);
            _channel.QueueDeclare(CreateDocumentQueueName, true, false, false);
            _channel.QueueBind(CreateDocumentQueueName, DocumentCreateExchange, RoutingDocument);
            
            return _channel;
        }
        private IModel GetChannel()
        {
            return _connection.CreateModel();
        }

        public void Dispose()
        {
            _channel?.Close();
            _channel?.Dispose();

            _connection?.Close();
            _connection?.Dispose();

            _logger.LogInformation("RabbitMQ Disconnected");
        }
    }
}

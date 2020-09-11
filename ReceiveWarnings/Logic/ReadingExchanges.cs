using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ReceiveWarnings.Logic
{
    public class ReadingExchanges : IHostedService, IDisposable
    {
        private readonly ConnectionFactory factory;
        private readonly IConnection connection;
        private readonly IModel channel;
        private readonly ILogger logger;

        public ReadingExchanges(ILoggerFactory logger)
        {
            this.logger = logger.CreateLogger<ReadingExchanges>();
            factory = new ConnectionFactory() { HostName = "localhost" };
            connection = factory.CreateConnection();
            channel = connection.CreateModel();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            channel.ExchangeDeclare(exchange: "direct_logsTwo", type: "direct");
            var queueName = channel.QueueDeclare().QueueName;

            channel.QueueBind(queue: queueName, exchange: "direct_logsTwo", routingKey: "warning");
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var routingLey = ea.RoutingKey;
                logger.LogInformation($" [x] Received {routingLey} - {message}");
            };

            channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken) => throw new NotImplementedException();

        public void Dispose()
        {
            channel?.Dispose();
            connection?.Dispose();
        }
    }
}
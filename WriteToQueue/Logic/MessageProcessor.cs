using Microsoft.AspNetCore.Http;
using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WriteToQueue.Logic
{
    public class MessageProcessor
    {
        public int SendMessage(string routingKey, string Message)
        {
            try
            {
                var factory = new ConnectionFactory() { HostName = "localhost" };

                using (var connection = factory.CreateConnection())
                {
                    using (var channel = connection.CreateModel())
                    {
                        channel.ExchangeDeclare(exchange: "direct_logsTwo", type: "direct");

                        var body = Encoding.UTF8.GetBytes(Message);

                        channel.BasicPublish(
                            exchange: "direct_logsTwo",
                            routingKey: routingKey,
                            basicProperties: null,
                            body: body
                            );

                        return StatusCodes.Status200OK;
                    }
                }
            }
            catch (BrokerUnreachableException)
            {
                return StatusCodes.Status400BadRequest;
            }
        }
    }
}

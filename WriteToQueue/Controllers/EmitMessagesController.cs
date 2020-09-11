using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;

namespace WriteToQueue.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EmitMessagesController : ControllerBase
    {
        [HttpPost]
        public IActionResult SendError()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };

            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.ExchangeDeclare(exchange: "direct_logsTwo", type: "direct");

                    var message = "This is a error message";
                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(
                        exchange: "direct_logsTwo",
                        routingKey: "error",
                        basicProperties: null,
                        body: body
                        );

                    return Ok("Message Sent");
                }
            }
        }

        [HttpPost]
        public IActionResult SendWarning()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };

            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.ExchangeDeclare(exchange: "direct_logsTwo", type: "direct");

                    var message = "This is a warning message";
                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(
                        exchange: "direct_logsTwo",
                        routingKey: "warning",
                        basicProperties: null,
                        body: body
                        );

                    return Ok("Message Sent");
                }
            }
        }
    }
}

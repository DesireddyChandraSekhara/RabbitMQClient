using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQProducer
{
    public static class RMProducer
    {
        public static void Producer()
        {

            var factory = new ConnectionFactory
            {
                HostName = "localhost"
            };
            //Create the RabbitMQ connection using connection factory details as i mentioned above
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();
            // channel.QueueDeclare("demo-queueHM2", durable: true, exclusive: false, autoDelete: false, arguments: null);
            channel.QueueDeclare("POC", durable: true, exclusive: false, autoDelete: false, arguments: null);
            var message = new { name = "producer", message = "POCDemo" };
            string msg = "Hi from poc";
            var body = Encoding.UTF8.GetBytes(msg);
            channel.BasicPublish("", "POC", null, body);
            Console.WriteLine(msg);

           
        }
    }
}

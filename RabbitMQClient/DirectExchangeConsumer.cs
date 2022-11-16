using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQClient
{
    public static class DirectExchangeConsumer
    {

        public static void Consumer()
        {

            var factory = new ConnectionFactory
            {
                HostName = "localhost"
            };
            //Create the RabbitMQ connection using connection factory details as i mentioned above
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();
            
            channel.ExchangeDeclare("POC-direct-Echange", ExchangeType.Direct);
            channel.QueueDeclare("POC-direct-Queue", durable: true, exclusive: false, autoDelete: false, arguments: null);
            channel.QueueBind("POC-direct-Queue", "POC-direct-Echange","");
            channel.BasicQos(0, 10, false);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (sender, args) =>
            {
                var body = args.Body.ToArray();
                var message=Encoding.UTF8.GetString(body);
                Console.WriteLine(message);
            };

            

        }
    }
}

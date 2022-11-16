using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQClient
{
    public static class FanoutExchange
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

            channel.ExchangeDeclare("POC-Fanout-Echange", ExchangeType.Fanout);
            channel.QueueDeclare("POC-Fanout-Queue", durable: true, exclusive: false, autoDelete: false, arguments: null);
            channel.QueueBind("POC-Fanout-Queue", "POC-Fanout-Echange", string.Empty);
            channel.BasicQos(0, 10, false);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (sender, args) =>
            {
                var body = args.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine(message);
            };

            channel.BasicConsume("POC-Fanout-Queue", true, consumer);
            Console.WriteLine("Fanout Exchange Consumer Started");
            Console.ReadLine();

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Net.NetworkInformation;


namespace RabbitMQClient
{
    public class RMConsumer
    {
      public   IModel Consumer()
        {
            var factory = new ConnectionFactory
            {
                HostName = "localhost"
            };
            //Create the RabbitMQ connection using connection factory details as i mentioned above
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();
            //declare the queue after mentioning name and a few property related to that
            //channel.QueueDeclare("POC", exclusive: false);
            channel.QueueDeclare("POC", durable: true, exclusive: false, autoDelete: false, arguments: null);

            //Set Event object which listen message from chanel which is sent by producer
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, eventArgs) =>
            {
                var body = eventArgs.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine("Message from Publisher/producer:" + message);
            };
            //read the message
            channel.BasicConsume(queue: "POC", autoAck: true, consumer: consumer);
            Console.ReadKey();
            return channel;
        }
    }
}





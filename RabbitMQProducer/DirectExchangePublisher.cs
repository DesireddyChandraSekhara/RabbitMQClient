using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQProducer
{
    public static class DirectExchangePublisher
    {
        public static void Publish()
        {

            var factory = new ConnectionFactory
            {
                HostName = "localhost"
            };
            //Create the RabbitMQ connection using connection factory details as i mentioned above
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();
            var ttl = new Dictionary<string, Object>
            {
                {"x-message-ttl",3000 }

            };
            channel.ExchangeDeclare("POC-direct-Echange", ExchangeType.Direct, arguments: ttl);
            var count = 0;
            while(true)
            {
                var message = new { Name = "Producer", Message = $"Hello Count: {count}" };
                var body=Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));
                channel.BasicPublish("POC-direct-Echange", "account.init", null, body);
                count++;
                Thread.Sleep(1000);
            }

        }


    }
}

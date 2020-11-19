using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Receiver
{
    internal class ReceiveLogs
    {
        private static void Main(string[] args)
        {
            var factory = new ConnectionFactory();

            using var connection = factory.CreateConnection();
            using var chanel = connection.CreateModel();

            chanel.ExchangeDeclare("logs", ExchangeType.Fanout);

            var queueName = chanel.QueueDeclare().QueueName;
            chanel.QueueBind(queueName, "logs", "");

            Console.WriteLine(" [*] Waiting for logs.");

            var consumer = new EventingBasicConsumer(chanel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine(" [x] {0}", message);
            };

            chanel.BasicConsume(queueName, true, consumer);

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }
    }
}

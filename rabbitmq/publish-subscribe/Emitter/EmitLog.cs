using System;
using System.Text;
using RabbitMQ.Client;

namespace Emitter
{
    internal class EmitLog
    {
        private static void Main(string[] args)
        {
            var factory = new ConnectionFactory { HostName = "localhost" };

            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.ExchangeDeclare("logs", ExchangeType.Fanout);

            Console.WriteLine(" [*] Press [Q] to exit.");

            while (true)
            {
                Console.WriteLine(" [*] Enter new message: ");
                var message = Console.ReadLine();

                if (string.IsNullOrEmpty(message))
                {
                    Console.WriteLine(" [!] Message is null or empty. ");
                    continue;
                }

                if (message.ToUpper().StartsWith('Q'))
                {
                    Environment.Exit(0);
                }

                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish("logs", "", null, body);

                Console.WriteLine(" [x] Sent {0}", message);
            }
        }
    }
}

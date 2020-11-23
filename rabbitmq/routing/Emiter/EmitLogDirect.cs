using System;
using System.Linq;
using System.Text;
using RabbitMQ.Client;

namespace Emiter
{
    class EmitLogDirect
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory { HostName = "localhost" };

            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.ExchangeDeclare("direct_logs", "direct");

            var severity = (args.Length > 0) ? args[0] : "info";
            var messages = (args.Length > 1) ? string.Join(" ", args.Skip(1).ToArray()) : "Hello RabbitMQ";

            var body = Encoding.UTF8.GetBytes(messages);
            channel.BasicPublish("direct_logs", severity, null, body);

            Console.WriteLine(" [x] Sent '{0}':'{1}'", severity, messages);
            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }
    }
}

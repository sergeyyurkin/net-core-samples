﻿using System;
using System.Text;
using System.Threading;
using RabbitMQ.Client;

namespace Producer
{
    public class Send
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };

            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(
                queue: "hello",
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            string message = "Hello World!";
            var body = Encoding.UTF8.GetBytes(message);

            int i = 0;
            while (i++ < 10)
            {
                channel.BasicPublish(
                exchange: "",
                routingKey: "hello",
                basicProperties: null,
                body: body);

                Console.WriteLine("{0}. [X] Sent {1}", i, message);

                Thread.Sleep(1000);
            }

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }
    }
}

using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace CommonLibrary
{
    public class RabbitMQService
    {

        private string _hostname = "127.0.0.1";
        // private string _username = "guest";
        // private string _password = "guest";
        public RabbitMQService()
        {

        }

        private IConnection GetRabbitMQConnection()
        {
            ConnectionFactory connectionFactory = new ConnectionFactory()
            {
                HostName = _hostname,
                // UserName = _username,
                // Password = _password,
                //Port = 1883
            };

            return connectionFactory.CreateConnection();
        }

        public void Publish(string queueName, string message)
        {
            Action act = () =>
            {
                using (var connection = this.GetRabbitMQConnection())
                {
                    using (var channel = connection.CreateModel())
                    {
                        channel.QueueDeclare(queueName, true, false, false, null);

                        channel.BasicPublish("", queueName, null, Encoding.UTF8.GetBytes(message));
                    }
                }
            };
            act.ExecuteCircuitPattern();
        }

        public IConnection Consume(string queueName, Action<string> act)
        {
            var connection = this.GetRabbitMQConnection();
            var channel = connection.CreateModel();

            channel.QueueDeclare(queueName, true, false, false, null);
            var consumer = new RabbitMQ.Client.Events.EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var _body = ea.Body.ToArray();
                var _message = Encoding.UTF8.GetString(_body);//serialized string

                act(_message);

                channel.BasicAck(ea.DeliveryTag, false);
            };
            channel.BasicConsume(queue: queueName,
                                 autoAck: false,
                                 consumer: consumer);

            return connection;
        }

    }
}

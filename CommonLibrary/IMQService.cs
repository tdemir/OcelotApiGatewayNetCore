using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLibrary
{
    public interface IMQService
    {
        void Publish(string queueName, string message);
        IConnection Consume(string queueName, Action<string> act);
    }
}

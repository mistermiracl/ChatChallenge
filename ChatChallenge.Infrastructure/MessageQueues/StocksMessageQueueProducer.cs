using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using ChatChallenge.Domain.Contracts.MessageQueues;
using System.Text;

namespace ChatChallenge.Infrastructure.MessageQueues;

public class StocksMessageQueueProducer : IStocksMessageQueueProducer
{
    private readonly string queue;
    private readonly IModel channel;
    
    public StocksMessageQueueProducer(string hostname, int port, string queue)
    {
        var factory = new ConnectionFactory
        {
            HostName = hostname,
            Port = port
        };
        this.queue = queue;
        var connection = factory.CreateConnection();
        channel = connection.CreateModel();
        channel.QueueDeclare(queue, exclusive: false);
    }

    public void SendMessage(string message)
    {
        channel.BasicPublish(exchange: "", routingKey: queue, body: Encoding.UTF8.GetBytes(message));
    }
}
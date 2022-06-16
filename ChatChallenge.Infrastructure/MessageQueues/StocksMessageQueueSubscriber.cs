using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using ChatChallenge.Domain.Contracts.MessageQueues;

namespace ChatChallenge.Infrastructure.MessageQueues;

public class StocksMessageQueueSubscriber : IStocksMessageQueueSubscriber
{
    private readonly string queue;
    private readonly IModel channel;
    private readonly EventingBasicConsumer consumer;
    
    public StocksMessageQueueSubscriber(string hostname, int port, string queue)
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
        
        consumer = new EventingBasicConsumer(channel);
        channel.BasicConsume(queue: queue, autoAck: true, consumer: consumer);
    }
    
    public void ReceiveMessage(Action<string> action)
    {
        consumer.Received += (model, eventArgs) =>
        {
            var body = eventArgs.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);

            action(message);
        };
    }
}
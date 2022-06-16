using ChatChallenge.Application.Contracts.Services;
using ChatChallenge.Domain.Contracts.MessageQueues;

namespace ChatChallenge.Application.Services;

public class StocksMessageQueueProducerService : IStocksMessageQueueProducerService
{
    private readonly IStocksMessageQueueProducer messageQueueProducer;
    public StocksMessageQueueProducerService(IStocksMessageQueueProducer messageQueueProducer)
    {
        this.messageQueueProducer = messageQueueProducer;
    }

    public void SendMessage(string message)
    {
        this.messageQueueProducer.SendMessage(message);
    }
}
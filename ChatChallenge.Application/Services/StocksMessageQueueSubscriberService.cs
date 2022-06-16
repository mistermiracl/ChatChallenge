using ChatChallenge.Application.Contracts.Services;
using ChatChallenge.Domain.Contracts.MessageQueues;

namespace ChatChallenge.Application.Services;

public class StocksMessageQueueSubscriberService : IStocksMessageQueueSubscriberService
{
    private readonly IStocksMessageQueueSubscriber stocksMessageQueueSubscriber;
    public StocksMessageQueueSubscriberService(IStocksMessageQueueSubscriber stocksMessageQueueSubscriber)
    {
        this.stocksMessageQueueSubscriber = stocksMessageQueueSubscriber;
    }

    public void ReceiveMessage(Action<string> action)
    {
        this.stocksMessageQueueSubscriber.ReceiveMessage(action);
    }
}
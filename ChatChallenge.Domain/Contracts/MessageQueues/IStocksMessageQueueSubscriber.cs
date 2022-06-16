namespace ChatChallenge.Domain.Contracts.MessageQueues;

public interface IStocksMessageQueueSubscriber
{
    public void ReceiveMessage(Action<string> action);
}
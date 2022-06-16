namespace ChatChallenge.Domain.Contracts.MessageQueues;

public interface IStocksMessageQueueProducer
{
    public void SendMessage(string message);
}
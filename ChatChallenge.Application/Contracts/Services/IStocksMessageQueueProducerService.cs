namespace ChatChallenge.Application.Contracts.Services;

public interface IStocksMessageQueueProducerService
{
    void SendMessage(string message);
}
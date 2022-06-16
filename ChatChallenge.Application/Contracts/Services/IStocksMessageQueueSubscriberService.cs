namespace ChatChallenge.Application.Contracts.Services;

public interface IStocksMessageQueueSubscriberService
{
    void ReceiveMessage(Action<string> action);
}
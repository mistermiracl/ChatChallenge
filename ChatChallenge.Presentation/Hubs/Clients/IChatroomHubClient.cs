using ChatChallenge.Presentation.Models;

namespace ChatChallenge.Presentation.Hubs.Clients;

public interface IChatroomHubClient
{
    public Task ReceiveMessage(MessageModel message);
    public Task ReceiveStocksMessage(StocksResponseModel message);
}
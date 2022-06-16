using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using ChatChallenge.Presentation.Hubs;
using ChatChallenge.Presentation.Hubs.Clients;
using ChatChallenge.Presentation.Models;
using ChatChallenge.Application.Contracts.Services;

namespace ChatChallenge.Presentation.BackgroundServices;

public class StocksBackgroundService : BackgroundService
{
    private readonly IStocksMessageQueueSubscriberService stocksMessageQueueSubscriberService;
    private readonly IHubContext<ChatroomHub, IChatroomHubClient> chatroomHub;
    private readonly ILogger<StocksBackgroundService> logger;
    
    public StocksBackgroundService
    (
        IHubContext<ChatroomHub, 
        IChatroomHubClient> chatroomHub,
        ILogger<StocksBackgroundService> logger,
        IServiceScopeFactory scopeFactory
    )
    {
        this.stocksMessageQueueSubscriberService = scopeFactory.CreateScope().ServiceProvider.GetRequiredService<IStocksMessageQueueSubscriberService>();
        this.chatroomHub = chatroomHub;
        this.logger = logger;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        stoppingToken.ThrowIfCancellationRequested();
        stocksMessageQueueSubscriberService.ReceiveMessage(async message => {
            Console.WriteLine("Received message!");
            Console.WriteLine(message);
            var stocksMQResponse = JsonConvert.DeserializeObject<StocksMQResponseModel>(message);
            var stocksBotReturnValue = JsonConvert.DeserializeObject<RequestStocksReturnValueModel>(stocksMQResponse.returnValue);
            var stocksResponse = new StocksResponseModel
            {
                success = stocksMQResponse.success,
                username = stocksBotReturnValue.username
            };
            if(stocksMQResponse.success)
            {
                stocksResponse.stockCode = stocksMQResponse.stockCode;
                stocksResponse.quote = stocksMQResponse.quote.Value;
                await chatroomHub.Clients.Group(stocksBotReturnValue.chatroomId).ReceiveStocksMessage(stocksResponse);
            }
            else
            {
                await chatroomHub.Clients.Client(stocksBotReturnValue.connectionId).ReceiveStocksMessage(stocksResponse);
            }
        });
        return Task.CompletedTask;
    }
}
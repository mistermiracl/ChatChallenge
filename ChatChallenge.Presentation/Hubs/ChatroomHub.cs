using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using ChatChallenge.Presentation.Models;
using ChatChallenge.Presentation.Hubs.Clients;
using ChatChallenge.Application.Contracts.Services;
using ChatChallenge.Domain.Entities;

namespace ChatChallenge.Presentation.Hubs;

[Authorize]
public class ChatroomHub : Hub<IChatroomHubClient>
{
    private readonly IChatroomService chatroomService;
    private readonly IMessageService messageService;
    private readonly UserManager<IdentityUser> userManager;
    private readonly Config config;
    private readonly HttpClient httpClient;
    public ChatroomHub(IChatroomService chatroomService, IMessageService messageService, UserManager<IdentityUser> userManager, Config config)
    {
        this.chatroomService = chatroomService;
        this.messageService = messageService;
        this.userManager = userManager;
        this.config = config;
        httpClient = new HttpClient(new HttpClientHandler {
            ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
        });
    }
    public async Task SendMessage(int chatroomId, MessageModel sentMessage)
    {
        Console.WriteLine(chatroomId);
        Console.WriteLine(sentMessage.Payload);
        Console.WriteLine(Context.UserIdentifier);
        sentMessage.SentTimestamp = DateTime.Now;
        var user = await userManager.FindByIdAsync(Context.UserIdentifier);
        var message = new Message {
            Payload = sentMessage.Payload,
            SentTimestamp = sentMessage.SentTimestamp.Value,
            User = user
        };
        await messageService.Create(chatroomId, message);
        await Clients.Groups(chatroomId.ToString()).ReceiveMessage(sentMessage);
    }

    public async Task<IEnumerable<MessageModel>> JoinGroup(int chatroomId)
    {
        Console.WriteLine("Current logged in user: " + Context.User.Identity.Name);
        await Groups.AddToGroupAsync(Context.ConnectionId, chatroomId.ToString());
        var first50 = (await chatroomService.GetWithLast50Messages(chatroomId)).Messages;
        var messages = first50.Select(m => new MessageModel{
            Username = m.User.UserName,
            Payload = m.Payload,
            SentTimestamp = m.SentTimestamp,
            Me = m.User.UserName == Context.User.Identity.Name
        });
        return messages;
    }
    
    public async Task<StocksBotResponseModel> RequestStocks(int chatroomId, RequestStocksModel body)
    {
        body.returnValue = JsonConvert.SerializeObject(new RequestStocksReturnValueModel {
            connectionId = Context.ConnectionId,
            chatroomId = chatroomId.ToString(),
            username = Context.User.Identity.Name
        });
        var response = await httpClient.PostAsJsonAsync<RequestStocksModel>(config.StocksBotUrl, body);
        return await response.Content.ReadFromJsonAsync<StocksBotResponseModel>();
    }
}
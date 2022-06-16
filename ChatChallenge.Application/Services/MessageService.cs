using ChatChallenge.Domain.Entities;
using ChatChallenge.Domain.Contracts.Repositories;
using ChatChallenge.Application.Contracts.Services;

namespace ChatChallenge.Application.Services;

public class MessageService : IMessageService
{
    private readonly IMessageRepository messageRepository;

    public MessageService(IMessageRepository messageRepository)
    {
        this.messageRepository = messageRepository;
    }

    public Task<int> Create(int chatroomId, Message message)
    {
        return messageRepository.Create(chatroomId, message);
    }
}
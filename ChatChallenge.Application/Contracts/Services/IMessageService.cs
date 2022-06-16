using ChatChallenge.Domain.Entities;

namespace ChatChallenge.Application.Contracts.Services;

public interface IMessageService
{
    Task<int> Create(int chatroomId, Message message);
}
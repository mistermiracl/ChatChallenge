using ChatChallenge.Domain.Entities;

namespace ChatChallenge.Domain.Contracts.Repositories;

public interface IMessageRepository : IBaseRepository<Message>
{
    Task<int> Create(int chatroomId, Message entity);
}
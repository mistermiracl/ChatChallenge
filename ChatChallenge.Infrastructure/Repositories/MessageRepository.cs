using ChatChallenge.Domain.Contracts.Repositories;
using ChatChallenge.Domain.Entities;
using ChatChallenge.Infrastructure.Data;

namespace ChatChallenge.Infrastructure.Repositories;

public class MessageRepository : IMessageRepository
{
    private readonly ChatChallengeDbContext context;
    
    public MessageRepository(ChatChallengeDbContext context)
    {
        this.context = context;
    }

    public async Task<int> Create(int chatroomId, Message entity)
    {
        var chatroom = await context.Chatrooms.FindAsync(chatroomId);
        entity.Chatroom = chatroom;
        context.Messages.Add(entity);
        await context.SaveChangesAsync();
        return entity.ID;
    }

    public Task<int> Create(Message entity)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Delete(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Message> Get(int id)
    {
        throw new NotImplementedException();
    }

    public Task<ICollection<Message>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<bool> Update(Message entity)
    {
        throw new NotImplementedException();
    }
}
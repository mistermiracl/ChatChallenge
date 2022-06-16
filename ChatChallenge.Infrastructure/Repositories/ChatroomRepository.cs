using Microsoft.EntityFrameworkCore;
using ChatChallenge.Domain.Contracts.Repositories;
using ChatChallenge.Domain.Entities;
using ChatChallenge.Infrastructure.Data;

namespace ChatChallenge.Infrastructure.Repositories;

public class ChatroomRepository : IChatroomRepository
{
    private readonly ChatChallengeDbContext context;
    
    public ChatroomRepository(ChatChallengeDbContext context)
    {
        this.context = context;
    }

    public async Task<int> Create(Chatroom entity)
    {
        context.Chatrooms.Add(entity);
        await context.SaveChangesAsync();
        return entity.ID;
    }

    public async Task<Chatroom> GetWithLast50Messages(int id)
    {
        var chatroom = await context.Chatrooms
            .Include(c => c.Messages
                .OrderBy(m => m.SentTimestamp)
                .Take(50)
            )
            // .ThenInclude(m => m.User)
            .FirstAsync(c => c.ID == id);
        return chatroom;
    }

    public Task<bool> Delete(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<Chatroom> Get(int id)
    {
        var chatroom = await context.Chatrooms.FindAsync(id);
        return chatroom;
    }

    public async Task<ICollection<Chatroom>> GetAll()
    {
        return await context.Chatrooms.ToListAsync();
    }

    public Task<bool> Update(Chatroom entity)
    {
        throw new NotImplementedException();
    }
}
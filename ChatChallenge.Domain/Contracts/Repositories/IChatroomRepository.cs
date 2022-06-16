using ChatChallenge.Domain.Entities;

namespace ChatChallenge.Domain.Contracts.Repositories;

public interface IChatroomRepository : IBaseRepository<Chatroom>
{
    Task<Chatroom> GetWithLast50Messages(int id);
}

using ChatChallenge.Domain.Entities;

namespace ChatChallenge.Application.Contracts.Services;

public interface IChatroomService
{
    Task<int> Create(Chatroom chatroom);
    Task<Chatroom> Get(int id);
    Task<Chatroom> GetWithLast50Messages(int id);
    Task<ICollection<Chatroom>> GetAll();
}
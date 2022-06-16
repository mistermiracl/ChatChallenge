using ChatChallenge.Domain.Entities;
using ChatChallenge.Domain.Contracts.Repositories;
using ChatChallenge.Application.Contracts.Services;

namespace ChatChallenge.Application.Services;

public class ChatroomService : IChatroomService
{
    private readonly IChatroomRepository chatroomRepository;
    public ChatroomService(IChatroomRepository chatroomRepository)
    {
        this.chatroomRepository = chatroomRepository;
    }
    public Task<int> Create(Chatroom chatroom)
    {
        return chatroomRepository.Create(chatroom);
    }

    public Task<Chatroom> Get(int id)
    {
        return chatroomRepository.Get(id);
    }

    public Task<Chatroom> GetWithLast50Messages(int id)
    {
        return chatroomRepository.GetWithLast50Messages(id);
    }

    public Task<ICollection<Chatroom>> GetAll()
    {
        return chatroomRepository.GetAll();
    }
}
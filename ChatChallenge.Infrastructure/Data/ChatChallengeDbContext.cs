using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using ChatChallenge.Domain.Entities;

namespace ChatChallenge.Infrastructure.Data;

public class ChatChallengeDbContext : IdentityDbContext
{
    public ChatChallengeDbContext(DbContextOptions<ChatChallengeDbContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseLazyLoadingProxies();
    }

    public DbSet<Chatroom> Chatrooms { get; set; }
    public DbSet<Message> Messages { get; set; }
}

using Microsoft.AspNetCore.Identity;

namespace ChatChallenge.Domain.Entities;

public class Message
{
    public int ID { get; set; }
    public string Payload { get; set; }
    public DateTime SentTimestamp { get; set; }
    public virtual IdentityUser User { get; set; }
    public virtual Chatroom Chatroom { get; set; }
}
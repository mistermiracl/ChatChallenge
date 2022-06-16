namespace ChatChallenge.Domain.Entities;
public class Chatroom
{
    public int ID { get; set; }
    public string Name { get; set; }
    public virtual ICollection<Message> Messages { get; set; }
}
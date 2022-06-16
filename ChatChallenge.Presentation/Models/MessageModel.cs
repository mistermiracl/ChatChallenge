namespace ChatChallenge.Presentation.Models;

public class MessageModel
{
    public string Username { get; set; }
    public string Payload { get; set; }
    public bool? Me { get; set; }
    public DateTime? SentTimestamp { get; set; }
}
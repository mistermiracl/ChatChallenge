namespace ChatChallenge.Presentation;
public class Config
{
    public readonly string StocksBotUrl;
    public readonly string StocksMessageQueueHostname;
    public readonly int StocksMessageQueuePort;
    public readonly string StocksMessageQueueName;
    public Config(IConfiguration configuration)
    {
        StocksBotUrl = configuration["InternalApis:StocksBotUrl"];
        StocksMessageQueueHostname = configuration["MessageQueues:StocksQueue:Hostname"];
        StocksMessageQueuePort = int.Parse(configuration["MessageQueues:StocksQueue:Port"]);
        StocksMessageQueueName = configuration["MessageQueues:StocksQueue:QueueName"];
    }
}
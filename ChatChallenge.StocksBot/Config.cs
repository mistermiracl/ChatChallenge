namespace ChatChallenge.StocksBot;

public class Config
{
    public readonly string StooqStocksApiUrl;
    public readonly string StocksMessageQueueHostname;
    public readonly int StocksMessageQueuePort;
    public readonly string StocksMessageQueueName;
    
    public Config(IConfiguration configuration)
    {
        StooqStocksApiUrl = configuration["Apis:StooqStocksApiUrl"];
        StocksMessageQueueHostname = configuration["MessageQueues:StocksQueue:Hostname"];
        StocksMessageQueuePort = int.Parse(configuration["MessageQueues:StocksQueue:Port"]);
        StocksMessageQueueName = configuration["MessageQueues:StocksQueue:QueueName"];
    }
}
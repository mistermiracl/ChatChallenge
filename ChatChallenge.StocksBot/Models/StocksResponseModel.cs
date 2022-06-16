namespace ChatChallenge.StocksBot.Models;

public class StocksResponseModel
{
    public bool success { get; set; }
    public dynamic returnValue { get; set; }
    public string stockCode { get; set; }
    public decimal quote { get; set; }
}
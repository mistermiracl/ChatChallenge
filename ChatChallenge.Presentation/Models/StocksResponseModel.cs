namespace ChatChallenge.Presentation.Models;

public class StocksResponseModel
{
    public bool success { get; set; }
    public string username { get; set; }
    public string stockCode { get; set; }
    public decimal quote { get; set; }
}
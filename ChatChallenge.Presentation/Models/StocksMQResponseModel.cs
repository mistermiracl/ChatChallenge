namespace ChatChallenge.Presentation.Models;

public class StocksMQResponseModel
{
    public bool success { get; set; }
    public string returnValue { get; set; }
    public string? stockCode { get; set; }
    public decimal? quote { get; set; }
}
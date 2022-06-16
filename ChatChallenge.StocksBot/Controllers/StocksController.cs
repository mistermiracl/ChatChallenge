using Microsoft.AspNetCore.Mvc;
using ChatChallenge.StocksBot.Models;
using ChatChallenge.Application.Contracts.Services;
using System.Text.Json;

namespace ChatChallenge.StocksBot.Controllers;

[ApiController]
[Route("[controller]")]
public class StocksController : ControllerBase
{
    private readonly Config config;
    private readonly IStocksMessageQueueProducerService stocksMessageQueueProducerService;
    private readonly ILogger<StocksController> logger;
    private readonly HttpClient httpClient = new HttpClient();

    public StocksController(Config config, IStocksMessageQueueProducerService stocksMessageQueueProducerService, ILogger<StocksController> logger)
    {
        this.config = config;
        this.stocksMessageQueueProducerService = stocksMessageQueueProducerService;
        this.logger = logger;
    }

    [HttpPost]// Name allows the controller to use it as default idk why though
    public StocksInitialResponseModel Post(StocksRequestModel body)
    {
        Console.WriteLine("The stock code is: " + body.stockCode);
        Console.WriteLine("The return value is: " + body.returnValue);
        PublishToMQ(body);
        return new StocksInitialResponseModel {
            success = true,
            message = "Ok"
        };
    }

    private void PublishToMQ(StocksRequestModel body)
    {
        Console.WriteLine("Executing...");
        httpClient.GetAsync(string.Format(config.StooqStocksApiUrl, body.stockCode)).ContinueWith(response => {
            response.Result.Content.ReadAsStringAsync().ContinueWith(csvResponse => {
                var stocksCsv = csvResponse.Result;
                Console.WriteLine(stocksCsv);
                var quote = GetQuoteFromCsv(stocksCsv);
                Console.WriteLine("the Quote is: " + quote);
                var message = new StocksResponseModel
                {
                    returnValue = body.returnValue
                };
                if(quote != null)
                {
                    message.success = true;
                    message.stockCode = body.stockCode;
                    message.quote = quote.Value;
                    
                } else {
                    message.success = false;
                }
                var jsonMessage = JsonSerializer.Serialize(message);
                stocksMessageQueueProducerService.SendMessage(jsonMessage);
            });
        });
        //var stocksCsv = await (await httpClient.GetAsync(string.Format(config.StooqStocksApiUrl, body.stockCode))).Content.ReadAsStringAsync();
    }

    private decimal? GetQuoteFromCsv(string stocksCsv) {
        string quoteString = stocksCsv.Split('\n')[1].Split(',')[4];
        decimal quote = 0;
        return decimal.TryParse(quoteString, out quote) ? quote : null;
    }
}

using ChatChallenge.Domain.Contracts.MessageQueues;
using ChatChallenge.Infrastructure.MessageQueues;
using ChatChallenge.Application.Contracts.Services;
using ChatChallenge.Application.Services;
using ChatChallenge.StocksBot;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<Config, Config>();
builder.Services.AddScoped<IStocksMessageQueueProducer>(serviceProvider => {
    var config = (Config)serviceProvider.GetRequiredService(typeof(Config));
    Console.WriteLine(config.StocksMessageQueueHostname);
    return new StocksMessageQueueProducer(
        config.StocksMessageQueueHostname, 
        config.StocksMessageQueuePort, 
        config.StocksMessageQueueName
    );
});
builder.Services.AddSingleton<IStocksMessageQueueProducerService>(serviceProvider => {
    var stocksMessageQueueProducer = (IStocksMessageQueueProducer)serviceProvider.CreateScope().ServiceProvider.GetRequiredService(typeof(IStocksMessageQueueProducer));
    return new StocksMessageQueueProducerService(stocksMessageQueueProducer);
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

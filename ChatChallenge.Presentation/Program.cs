using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ChatChallenge.Infrastructure.Data;
using ChatChallenge.Infrastructure.Repositories;
using ChatChallenge.Infrastructure.MessageQueues;
using ChatChallenge.Domain.Contracts.Repositories;
using ChatChallenge.Domain.Contracts.MessageQueues;
using ChatChallenge.Application.Contracts.Services;
using ChatChallenge.Application.Services;
using ChatChallenge.Presentation;
using ChatChallenge.Presentation.Hubs;
using ChatChallenge.Presentation.BackgroundServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddSingleton<Config, Config>();
builder.Services.AddDbContext<ChatChallengeDbContext>(options => options.UseSqlite(connectionString));
// Identity config
builder.Services.AddDefaultIdentity<IdentityUser>(
    options => {
        options.Password.RequireDigit = false;
        options.Password.RequiredLength = 5;
        options.Password.RequireUppercase = false;
        options.Password.RequireNonAlphanumeric = false;
        options.SignIn.RequireConfirmedAccount = false;
    }
)
.AddEntityFrameworkStores<ChatChallengeDbContext>();
// Dependencies
builder.Services.AddScoped<IChatroomRepository, ChatroomRepository>();
builder.Services.AddScoped<IMessageRepository, MessageRepository>();
builder.Services.AddScoped<IMessageService, MessageService>();
builder.Services.AddScoped<IChatroomService, ChatroomService>();
builder.Services.AddScoped<IStocksMessageQueueSubscriber>(serviceProvider => {
    var config = (Config)serviceProvider.GetService(typeof(Config))!;
    return new StocksMessageQueueSubscriber(
        config.StocksMessageQueueHostname, 
        config.StocksMessageQueuePort, 
        config.StocksMessageQueueName
    );
});
builder.Services.AddScoped<IStocksMessageQueueSubscriberService, StocksMessageQueueSubscriberService>();
// Background Services
builder.Services.AddHostedService<StocksBackgroundService>();

builder.Services.AddControllersWithViews();
builder.Services.AddSignalR()
    .AddJsonProtocol(options => options.PayloadSerializerOptions.PropertyNamingPolicy = null);

if(builder.Environment.IsDevelopment()) {
    builder.Services.AddDatabaseDeveloperPageExceptionFilter();
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsProduction())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
} else {
    app.UseDeveloperExceptionPage();
}

app.UseStatusCodePages();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);
app.MapRazorPages();
app.MapHub<ChatroomHub>("/chatroomHub");

app.Run();

using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ChatChallenge.Presentation.Models;
using ChatChallenge.Application.Contracts.Services;

namespace ChatChallenge.Presentation.Controllers;

public class HomeController : Controller
{
    private readonly IChatroomService chatroomService;
    private readonly ILogger<HomeController> logger;

    public HomeController(IChatroomService chatroomService, ILogger<HomeController> logger)
    {
        this.chatroomService = chatroomService;
        this.logger = logger;
    }

    public async Task<IActionResult> Index()
    {
        ViewBag.Chatrooms = await chatroomService.GetAll();
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

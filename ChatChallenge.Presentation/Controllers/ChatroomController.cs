using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ChatChallenge.Application.Contracts.Services;
using ChatChallenge.Domain.Entities;

namespace ChatChallenge.Presentation.Controllers;

[Authorize]
public class ChatroomController : Controller
{
    private readonly IChatroomService chatroomService;
    private readonly ILogger<ChatroomController> logger;
    
    public ChatroomController(IChatroomService chatroomService, Config config, ILogger<ChatroomController> logger)
    {
        this.chatroomService = chatroomService;
        this.logger = logger;
    }

    public async Task<IActionResult> Chat(int? id)
    {
        if(id != null) {
            var chatroom = await chatroomService.Get(id.Value);
            ViewBag.username = User.Identity.Name;
            return View(chatroom);
        }
        return Redirect("/");
    }

    [HttpPost]
    public async Task<IActionResult> Create(string name)
    {
        Chatroom chatroom = new Chatroom {
            Name = name
        };
        int id = await chatroomService.Create(chatroom);
        return RedirectToAction("Chat", "Chatroom", new { id });
    }
}
using ChatServer.Webapı.Context;
using ChatServer.Webapı.Dtos;
using ChatServer.Webapı.Hubs;
using ChatServer.Webapı.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace ChatServer.Webapı.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public  sealed class ChatsController (
        AppDbContext context,
        IHubContext<ChatHub> hubContext): ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetChats(Guid userId,Guid toUserId, CancellationToken cancellationToken)
        {
            List<Chat> chats =
                await context
                .Chats
                .Where(p=>
                p.UserId==userId&&p.ToUserId ==toUserId ||
                p.ToUserId==userId&&p.UserId==toUserId)
                .OrderBy(p=>p.Date)
                .ToListAsync(cancellationToken);

            return Ok(chats);
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage(SendMessageDto request,CancellationToken cancellationToken)
        {
            Chat chat = new()
            {
                UserId = request.UserId,
                ToUserId = request.ToUserId,
                Message = request.Message,
                Date = DateTime.Now
            };
            await context.AddAsync(chat,cancellationToken);
            await context.SaveChangesAsync(cancellationToken);

            string connectionId = ChatHub.Users.First(p => p.Value == chat.ToUserId).Key;

            await hubContext.Clients.Client(connectionId).SendAsync("Messages",chat);
            return Ok();
        }




    }
}

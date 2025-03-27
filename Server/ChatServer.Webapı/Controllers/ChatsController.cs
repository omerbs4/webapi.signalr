using ChatServer.Webapı.Context;
using ChatServer.Webapı.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ChatServer.Webapı.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public  sealed class ChatsController (
        AppDbContext context): ControllerBase
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
    }
}

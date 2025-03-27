﻿using ChatServer.Webapı.Context;
using ChatServer.Webapı.Dtos;
using ChatServer.Webapı.Models;
using GenericFileService.Files;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ChatServer.Webapı.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public sealed class AuthController(
        AppDbContext context) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Register(RegisterDto request, CancellationToken cancellationToken)
        {
            bool isNameExists = await context.Users.AnyAsync(p => p.Name == request.Name, cancellationToken);
            if (isNameExists)
            {
                return BadRequest(new { Message = "Bu kullaniic daha once kullanilmis ! " });
            }
            string avatar = FileService.FileSaveToServer(request.File, "wwwroot/avatar/");

            User user = new()
            {
                Name = request.Name,
                Avatar = avatar
            };
            await context.Users.AddAsync(user, cancellationToken);
            await context.SaveChangesAsync(); ;


            return NoContent();
        }

        //giris metotu
        [HttpGet]
        public async Task<IActionResult> Login(string name, CancellationToken cancellationToken)
        {
            User? user = await context.Users.FirstOrDefaultAsync(p => p.Name == name, cancellationToken);
            if (user is null)
            {
                return BadRequest(new { Message = "kullanici bulunamadi ! " });

            }
            user.Status = "online";

            await context.SaveChangesAsync(cancellationToken);
            return Ok(user);
        }
    }
}

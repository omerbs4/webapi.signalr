﻿using ChatServer.Webapı.Context;
using ChatServer.Webapı.Models;
using Microsoft.AspNetCore.SignalR;

namespace ChatServer.Webapı.Hubs
{
    public sealed class ChatHub(AppDbContext context) : Hub

    {

        public static Dictionary<string, Guid> Users = new();
        public async Task Connect(Guid userId)
        {
            Users.Add( Context.ConnectionId, userId);
            User? user = await context.Users.FindAsync(userId);
            if (user is not null)
            {
                user.Status = "online";
                await context.SaveChangesAsync();

                await Clients.All.SendAsync("Users", user);
            }

        }
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            Guid userId;
            Users.TryGetValue(Context.ConnectionId,out userId);
            User? user = await context.Users.FindAsync(userId);
            if (user is not null)
            {
                user.Status = "offline";
                await context.SaveChangesAsync();

                await Clients.All.SendAsync("Users", user);
            }

        }

    }

}

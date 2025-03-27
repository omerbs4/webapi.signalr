using ChatServer.Webapı.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ChatServer.Webapı.Context
{
    public sealed class AppDbContext : DbContext

    {
      

        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Chat> Chats { get; set; }

    }
}

using ChatServer.Webapý.Context;
using ChatServer.Webapý.Hubs;
using Microsoft.EntityFrameworkCore;
using Nlabs.DefaultCorsPolicy;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSignalR();
builder.Services.AddDefaultCors();
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("SqlServer")));
var app = builder.Build();




//app.MapGet("/", () => "Hello World!");
app.UseHttpsRedirection();
app.UseCors();
app.UseAuthorization();
app.MapControllers();
app.MapHub<ChatHub>("/chat-hub");

app.Run();

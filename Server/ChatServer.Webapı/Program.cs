using ChatServer.Webap�.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("SqlServer")));
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();

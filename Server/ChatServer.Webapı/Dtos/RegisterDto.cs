namespace ChatServer.Webapı.Dtos
{
    public sealed record RegisterDto (
        string Name,
        IFormFile File);
    
}

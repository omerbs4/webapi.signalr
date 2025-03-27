namespace ChatServer.Webapı.Models
{
    public sealed class Chat
    {
        public Chat()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public string ToUserId { get; set; }
        public string Message { get; set; } = default!;
        public DateTime Date { get; set; }

    }
}

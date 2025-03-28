﻿namespace ChatServer.Webapı.Models
{
    public sealed class User
    {
        public User()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public string Avatar { get; set; } = default!;
        public string Status { get; set; } = default!;
    }
}

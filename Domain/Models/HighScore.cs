using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class HighScore
    {
        public HighScore() { }
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime Datetime { get; set; } = DateTime.UtcNow;
        public int Score { get; set; }
        public string PlayerName { get; set; } = string.Empty;
        public Guid GameId { get; set; }
        public Guid? UserId { get; set; }
        public User? User { get; set; }
    }
}

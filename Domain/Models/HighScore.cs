using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class HighScore
    {
        public HighScore() { }
        public Guid Id { get; private set; } = Guid.NewGuid();
        public DateTime Datetime { get; private set; } = DateTime.UtcNow;
        public int Score { get; private set; }
        public User User { get; private set; }
    }
}

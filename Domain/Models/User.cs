using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class User
    {
        public User() { }
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public bool IsAdmin { get; set; } = false;

        public void SetAdmin(bool isAdmin)
        {
            IsAdmin = isAdmin;
        }

        // Navigation property – EF bruger den til cascade delete (se AppDbContext)
        public ICollection<HighScore> HighScores { get; private set; } = new List<HighScore>();
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class Game
    {
        public Game() { }
        public Guid Id { get; private set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Kategori Kategori { get; private set; }
    }
}

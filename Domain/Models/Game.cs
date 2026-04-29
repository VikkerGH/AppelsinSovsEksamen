using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class Game
    {
        public Game() { }
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Guid? KategoriId { get; set; }
        public Kategori? Kategori { get; set; }
    }
}

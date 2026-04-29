using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class Kategori
    {
        public Kategori() { }
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
    }
}

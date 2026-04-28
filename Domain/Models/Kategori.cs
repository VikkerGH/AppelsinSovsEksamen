using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class Kategori
    {
        public Kategori() { }
        public Guid Id { get; private set; }
        public string Name { get; private set; } = string.Empty;
    }
}

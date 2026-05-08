using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Models
{
    public class Review
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Du skal skrive noget i anmeldelsen")]
        public string Text { get; set; }

        public Guid GameId { get; set; }

        public Guid UserId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}

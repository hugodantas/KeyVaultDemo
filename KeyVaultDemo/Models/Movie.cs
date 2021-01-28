using System;

namespace KeyVaultDemo.Models
{
    public class Movie
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Director { get; set; }
        public DateTime ReleasedAt { get; set; }
    }
}

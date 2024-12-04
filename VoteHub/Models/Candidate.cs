using System.ComponentModel.DataAnnotations;

namespace VoteHubBG.Models
{
    public class Candidate
    {
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; }

        [Required, MaxLength(100)]
        public string Party { get; set; }

        [MaxLength(1000)]
        public string? Description { get; set; }

        public int Votes { get; set; } = 0;
    }
}

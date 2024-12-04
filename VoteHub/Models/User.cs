using System.ComponentModel.DataAnnotations;

namespace VoteHubBG.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string FirstName { get; set; }

        [Required, MaxLength(50)]
        public string LastName { get; set; }

        [Required, MaxLength(100), EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public bool HasVoted { get; set; } = false;
    }
}

using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using VoteHubBG.Models;

public class Vote
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string UserId { get; set; }

    [Required]
    public int CandidateId { get; set; }

    [Required]
    public DateTime VoteDate { get; set; } = DateTime.Now;

    [ForeignKey(nameof(UserId))]
    public IdentityUser User { get; set; }

    [ForeignKey(nameof(CandidateId))]
    public Candidate Candidate { get; set; }
}

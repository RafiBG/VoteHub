using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace VoteHubBG.Models
{
    public class VoteHubContext : IdentityDbContext<IdentityUser, IdentityRole, string>
    {
        public VoteHubContext(DbContextOptions<VoteHubContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<Vote> Votes { get; set; }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VoteHubBG.Models;

namespace VoteHubBG.Controllers
{
    [Authorize]
    public class VoteController : Controller
    {
        private readonly VoteHubContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public VoteController(VoteHubContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);
            var existingVote = await _context.Votes.FirstOrDefaultAsync(v => v.UserId == userId);

            if (existingVote != null)
            {
                // Show information for who he voted
                var candidate = await _context.Candidates.FirstOrDefaultAsync(c => c.Id == existingVote.CandidateId);
                ViewBag.VotedCandidate = candidate;
                return View("AlreadyVoted");
            }

            // Shows all the candidates
            var candidates = await _context.Candidates.ToListAsync();
            return View(candidates);
        }

        [HttpPost]
        public async Task<IActionResult> Vote(int candidateId)
        {
            var userId = _userManager.GetUserId(User);
            
            // Check if user voted
            if (await _context.Votes.AnyAsync(v => v.UserId == userId))
            {
                return RedirectToAction("Index");
            }

            var vote = new Vote
            {
                UserId = userId,
                CandidateId = candidateId,
                VoteDate = DateTime.Now
            };

            _context.Votes.Add(vote);

            var candidate = await _context.Candidates.FirstOrDefaultAsync(c => c.Id == candidateId);
            if (candidate != null)
            {
                candidate.Votes += 1;
            }

            await _context.SaveChangesAsync();

            //TempData["SuccessMessage"] = "Вашият глас беше успешно записан!";
            return RedirectToAction("Index");
        }

        [AllowAnonymous]
        public IActionResult AlreadyVoted()
        {
            return View();
        }
    }
}

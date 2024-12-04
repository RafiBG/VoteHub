using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using VoteHubBG.Models;
using System.Threading.Tasks;

[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
    private readonly VoteHubContext _context;

    public AdminController(VoteHubContext context)
    {
        _context = context;
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Candidate candidate)
    {
        if (ModelState.IsValid)
        {
            _context.Candidates.Add(candidate);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }
        return View(candidate);
    }
}
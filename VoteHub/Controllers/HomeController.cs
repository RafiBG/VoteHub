using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using VoteHub.Models;
using VoteHubBG.Models;

namespace VoteHub.Controllers
{
    public class HomeController : Controller
    {
        private readonly VoteHubContext _context;

        public HomeController(VoteHubContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var parties = await _context.Candidates.ToListAsync();
            return View(parties);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

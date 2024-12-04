using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using VoteHubBG.Models;
using System.Threading.Tasks;

[Authorize(Roles = "Admin")]
public class AdminCandidatesController : Controller
{
    private readonly VoteHubContext _context;

    public AdminCandidatesController(VoteHubContext context)
    {
        _context = context;
    }

    // GET: AdminCandidates/Index
    public async Task<IActionResult> Index()
    {
        var candidates = await _context.Candidates.ToListAsync();
        return View(candidates);
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
            return NotFound();

        var candidate = await _context.Candidates.FirstOrDefaultAsync(c => c.Id == id);

        if (candidate == null)
            return NotFound();

        return View(candidate);
    }

    // GET: AdminCandidates/Create
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
            return RedirectToAction(nameof(Index));
        }
        return View(candidate);
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
            return NotFound();

        var candidate = await _context.Candidates.FindAsync(id);

        if (candidate == null)
            return NotFound();

        return View(candidate);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Candidate candidate)
    {
        if (id != candidate.Id)
            return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                _context.Candidates.Update(candidate);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CandidateExists(candidate.Id))
                    return NotFound();
                else
                    throw;
            }
            return RedirectToAction(nameof(Index));
        }
        return View(candidate);
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
            return NotFound();

        var candidate = await _context.Candidates.FirstOrDefaultAsync(c => c.Id == id);

        if (candidate == null)
            return NotFound();

        return View(candidate);
    }

    // POST: AdminCandidates/Delete
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var candidate = await _context.Candidates.FindAsync(id);

        if (candidate != null)
        {
            _context.Candidates.Remove(candidate);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Index));
    }

    private bool CandidateExists(int id)
    {
        return _context.Candidates.Any(e => e.Id == id);
    }
}

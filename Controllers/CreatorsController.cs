using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using ComicMvC.Data;
using ComicMvC.Models;

namespace ComicMvC.Controllers
{
    public class CreatorsController : Controller
    {
        private readonly ComicsContext _context;

        public CreatorsController(ComicsContext context)
        {
            _context = context;
        }

        // Anyone can view the list
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Creators.ToListAsync());
        }

        // Anyone can view details
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var creator = await _context.Creators.FirstOrDefaultAsync(m => m.CreatorId == id);
            if (creator == null) return NotFound();

            return View(creator);
        }

        // Only registered users can create
        [Authorize(Policy = "RegisteredOnly")]
        public IActionResult Create() => View();

        [HttpPost]
        [Authorize(Policy = "RegisteredOnly")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CreatorId,FullName,Role")] Creator creator)
        {
            if (ModelState.IsValid)
            {
                _context.Add(creator);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(creator);
        }

        // Only registered users can edit
        [Authorize(Policy = "RegisteredOnly")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var creator = await _context.Creators.FindAsync(id);
            if (creator == null) return NotFound();

            return View(creator);
        }

        [HttpPost]
        [Authorize(Policy = "RegisteredOnly")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CreatorId,FullName,Role")] Creator creator)
        {
            if (id != creator.CreatorId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(creator);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CreatorExists(creator.CreatorId)) return NotFound();
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(creator);
        }

        // Only registered users can delete
        [Authorize(Policy = "RegisteredOnly")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var creator = await _context.Creators.FirstOrDefaultAsync(m => m.CreatorId == id);
            if (creator == null) return NotFound();

            return View(creator);
        }

        [HttpPost, ActionName("Delete")]
        [Authorize(Policy = "RegisteredOnly")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var creator = await _context.Creators.FindAsync(id);
            if (creator != null)
            {
                _context.Creators.Remove(creator);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool CreatorExists(int id) =>
            _context.Creators.Any(e => e.CreatorId == id);
    }
}

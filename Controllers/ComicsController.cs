using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using ComicMvC.Data;
using ComicMvC.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ComicMvC.Controllers
{
    public class ComicsController : Controller
    {
        private readonly ComicsContext _context;

        public ComicsController(ComicsContext context)
        {
            _context = context;
        }

        // Anyone can browse the list of comics
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var comics = await _context.Comics
                .Include(c => c.Genre)
                .Include(c => c.Publisher)
                .ToListAsync();
            return View(comics);
        }

        // Anyone can view details
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var comic = await _context.Comics
                .Include(c => c.Genre)
                .Include(c => c.Publisher)
                .FirstOrDefaultAsync(m => m.ComicId == id);
            if (comic == null) return NotFound();

            return View(comic);
        }

        // Only registered users can create
        [Authorize(Policy = "RegisteredOnly")]
        public IActionResult Create()
        {
            ViewData["GenreId"] = new SelectList(_context.Genres, "GenreId", "GenreName");
            ViewData["PublisherId"] = new SelectList(_context.Publishers, "PublisherId", "Name");
            return View();
        }

        [HttpPost]
        [Authorize(Policy = "RegisteredOnly")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ComicId,Title,IssueNumber,ReleaseDate,PublisherId,GenreId,CoverImageUrl,Synopsis")] Comic comic)
        {
            if (ModelState.IsValid)
            {
                _context.Add(comic);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GenreId"] = new SelectList(_context.Genres, "GenreId", "GenreName", comic.GenreId);
            ViewData["PublisherId"] = new SelectList(_context.Publishers, "PublisherId", "Name", comic.PublisherId);
            return View(comic);
        }

        // Only registered users can edit
        [Authorize(Policy = "RegisteredOnly")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var comic = await _context.Comics.FindAsync(id);
            if (comic == null) return NotFound();

            ViewData["GenreId"] = new SelectList(_context.Genres, "GenreId", "GenreName", comic.GenreId);
            ViewData["PublisherId"] = new SelectList(_context.Publishers, "PublisherId", "Name", comic.PublisherId);
            return View(comic);
        }

        [HttpPost]
        [Authorize(Policy = "RegisteredOnly")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ComicId,Title,IssueNumber,ReleaseDate,PublisherId,GenreId,CoverImageUrl,Synopsis")] Comic comic)
        {
            if (id != comic.ComicId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(comic);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ComicExists(comic.ComicId))
                        return NotFound();
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["GenreId"] = new SelectList(_context.Genres, "GenreId", "GenreName", comic.GenreId);
            ViewData["PublisherId"] = new SelectList(_context.Publishers, "PublisherId", "Name", comic.PublisherId);
            return View(comic);
        }

        // Only registered users can delete
        [Authorize(Policy = "RegisteredOnly")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var comic = await _context.Comics
                .Include(c => c.Genre)
                .Include(c => c.Publisher)
                .FirstOrDefaultAsync(m => m.ComicId == id);
            if (comic == null) return NotFound();

            return View(comic);
        }

        [HttpPost, ActionName("Delete")]
        [Authorize(Policy = "RegisteredOnly")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var comic = await _context.Comics.FindAsync(id);
            if (comic != null)
            {
                _context.Comics.Remove(comic);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool ComicExists(int id) =>
            _context.Comics.Any(e => e.ComicId == id);
    }
}

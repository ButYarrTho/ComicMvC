using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using ComicMvC.Data;
using ComicMvC.Models;

namespace ComicMvC.Controllers
{
    public class PublishersController : Controller
    {
        private readonly ComicsContext _context;

        public PublishersController(ComicsContext context)
        {
            _context = context;
        }

        // Anyone can view
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Publishers.ToListAsync());
        }

        // Anyone can view details
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var publisher = await _context.Publishers.FirstOrDefaultAsync(m => m.PublisherId == id);
            if (publisher == null) return NotFound();

            return View(publisher);
        }

        // Only registered users can create
        [Authorize(Policy = "RegisteredOnly")]
        public IActionResult Create() => View();

        [HttpPost]
        [Authorize(Policy = "RegisteredOnly")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PublisherId,Name,FoundedYear,Headquarters")] Publisher publisher)
        {
            if (ModelState.IsValid)
            {
                _context.Add(publisher);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(publisher);
        }

        // Only registered users can edit
        [Authorize(Policy = "RegisteredOnly")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var publisher = await _context.Publishers.FindAsync(id);
            if (publisher == null) return NotFound();

            return View(publisher);
        }

        [HttpPost]
        [Authorize(Policy = "RegisteredOnly")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PublisherId,Name,FoundedYear,Headquarters")] Publisher publisher)
        {
            if (id != publisher.PublisherId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(publisher);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PublisherExists(publisher.PublisherId)) return NotFound();
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(publisher);
        }

        // Only registered users can delete
        [Authorize(Policy = "RegisteredOnly")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var publisher = await _context.Publishers.FirstOrDefaultAsync(m => m.PublisherId == id);
            if (publisher == null) return NotFound();

            return View(publisher);
        }

        [HttpPost, ActionName("Delete")]
        [Authorize(Policy = "RegisteredOnly")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var publisher = await _context.Publishers.FindAsync(id);
            if (publisher != null)
            {
                _context.Publishers.Remove(publisher);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool PublisherExists(int id) =>
            _context.Publishers.Any(e => e.PublisherId == id);
    }
}

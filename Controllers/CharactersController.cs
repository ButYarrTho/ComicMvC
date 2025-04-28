using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using ComicMvC.Data;
using ComicMvC.Models;

namespace ComicMvC.Controllers
{
    public class CharactersController : Controller
    {
        private readonly ComicsContext _context;

        public CharactersController(ComicsContext context)
        {
            _context = context;
        }

        // GET: Characters
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var characters = await _context.Characters.ToListAsync();
            return View(characters);
        }

        // GET: Characters/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var character = await _context.Characters
                .FirstOrDefaultAsync(m => m.CharacterId == id);

            if (character == null) return NotFound();

            return View(character);
        }

        // GET: Characters/Create
        [Authorize(Policy = "RegisteredOnly")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Characters/Create
        [HttpPost]
        [Authorize(Policy = "RegisteredOnly")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Alias,Description")] Character character)
        {
            if (ModelState.IsValid)
            {
                _context.Add(character);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(character);
        }

        // GET: Characters/Edit/5
        [Authorize(Policy = "RegisteredOnly")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var character = await _context.Characters.FindAsync(id);
            if (character == null) return NotFound();

            return View(character);
        }

        // POST: Characters/Edit/5
        [HttpPost]
        [Authorize(Policy = "RegisteredOnly")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CharacterId,Name,Alias,Description")] Character character)
        {
            if (id != character.CharacterId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(character);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CharacterExists(character.CharacterId))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(character);
        }

        // GET: Characters/Delete/5
        [Authorize(Policy = "RegisteredOnly")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var character = await _context.Characters
                .FirstOrDefaultAsync(m => m.CharacterId == id);
            if (character == null) return NotFound();

            return View(character);
        }

        // POST: Characters/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Policy = "RegisteredOnly")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var character = await _context.Characters.FindAsync(id);
            if (character != null)
            {
                _context.Characters.Remove(character);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool CharacterExists(int id)
        {
            return _context.Characters.Any(e => e.CharacterId == id);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ComicMvC.Data;
using ComicMvC.Models;

namespace ComicMvC.Controllers
{
    public class ComicCharactersController : Controller
    {
        private readonly ComicsContext _context;

        public ComicCharactersController(ComicsContext context)
        {
            _context = context;
        }

        // GET: ComicCharacters
        public async Task<IActionResult> Index()
        {
            var comicsContext = _context.ComicCharacters.Include(c => c.Character).Include(c => c.Comic);
            return View(await comicsContext.ToListAsync());
        }

        // GET: ComicCharacters/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comicCharacter = await _context.ComicCharacters
                .Include(c => c.Character)
                .Include(c => c.Comic)
                .FirstOrDefaultAsync(m => m.ComicId == id);
            if (comicCharacter == null)
            {
                return NotFound();
            }

            return View(comicCharacter);
        }

        // GET: ComicCharacters/Create
        public IActionResult Create()
        {
            ViewData["CharacterId"] = new SelectList(_context.Characters, "CharacterId", "Name");
            ViewData["ComicId"] = new SelectList(_context.Comics, "ComicId", "Title");
            return View();
        }

        // POST: ComicCharacters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ComicId,CharacterId")] ComicCharacter comicCharacter)
        {
            if (ModelState.IsValid)
            {
                _context.Add(comicCharacter);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CharacterId"] = new SelectList(_context.Characters, "CharacterId", "Name", comicCharacter.CharacterId);
            ViewData["ComicId"] = new SelectList(_context.Comics, "ComicId", "Title", comicCharacter.ComicId);
            return View(comicCharacter);
        }

        // GET: ComicCharacters/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comicCharacter = await _context.ComicCharacters.FindAsync(id);
            if (comicCharacter == null)
            {
                return NotFound();
            }
            ViewData["CharacterId"] = new SelectList(_context.Characters, "CharacterId", "Name", comicCharacter.CharacterId);
            ViewData["ComicId"] = new SelectList(_context.Comics, "ComicId", "Title", comicCharacter.ComicId);
            return View(comicCharacter);
        }

        // POST: ComicCharacters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ComicId,CharacterId")] ComicCharacter comicCharacter)
        {
            if (id != comicCharacter.ComicId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(comicCharacter);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ComicCharacterExists(comicCharacter.ComicId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CharacterId"] = new SelectList(_context.Characters, "CharacterId", "Name", comicCharacter.CharacterId);
            ViewData["ComicId"] = new SelectList(_context.Comics, "ComicId", "Title", comicCharacter.ComicId);
            return View(comicCharacter);
        }

        // GET: ComicCharacters/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comicCharacter = await _context.ComicCharacters
                .Include(c => c.Character)
                .Include(c => c.Comic)
                .FirstOrDefaultAsync(m => m.ComicId == id);
            if (comicCharacter == null)
            {
                return NotFound();
            }

            return View(comicCharacter);
        }

        // POST: ComicCharacters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var comicCharacter = await _context.ComicCharacters.FindAsync(id);
            if (comicCharacter != null)
            {
                _context.ComicCharacters.Remove(comicCharacter);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ComicCharacterExists(int id)
        {
            return _context.ComicCharacters.Any(e => e.ComicId == id);
        }
    }
}

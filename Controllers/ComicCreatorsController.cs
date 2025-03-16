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
    public class ComicCreatorsController : Controller
    {
        private readonly ComicsContext _context;

        public ComicCreatorsController(ComicsContext context)
        {
            _context = context;
        }

        // GET: ComicCreators
        public async Task<IActionResult> Index()
        {
            var comicsContext = _context.ComicCreators.Include(c => c.Comic).Include(c => c.Creator);
            return View(await comicsContext.ToListAsync());
        }

        // GET: ComicCreators/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comicCreator = await _context.ComicCreators
                .Include(c => c.Comic)
                .Include(c => c.Creator)
                .FirstOrDefaultAsync(m => m.ComicId == id);
            if (comicCreator == null)
            {
                return NotFound();
            }

            return View(comicCreator);
        }

        // GET: ComicCreators/Create
        public IActionResult Create()
        {
            ViewData["ComicId"] = new SelectList(_context.Comics, "ComicId", "Title");
            ViewData["CreatorId"] = new SelectList(_context.Creators, "CreatorId", "FullName");
            return View();
        }

        // POST: ComicCreators/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ComicId,CreatorId")] ComicCreator comicCreator)
        {
            if (ModelState.IsValid)
            {
                _context.Add(comicCreator);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ComicId"] = new SelectList(_context.Comics, "ComicId", "Title", comicCreator.ComicId);
            ViewData["CreatorId"] = new SelectList(_context.Creators, "CreatorId", "FullName", comicCreator.CreatorId);
            return View(comicCreator);
        }

        // GET: ComicCreators/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comicCreator = await _context.ComicCreators.FindAsync(id);
            if (comicCreator == null)
            {
                return NotFound();
            }
            ViewData["ComicId"] = new SelectList(_context.Comics, "ComicId", "Title", comicCreator.ComicId);
            ViewData["CreatorId"] = new SelectList(_context.Creators, "CreatorId", "FullName", comicCreator.CreatorId);
            return View(comicCreator);
        }

        // POST: ComicCreators/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ComicId,CreatorId")] ComicCreator comicCreator)
        {
            if (id != comicCreator.ComicId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(comicCreator);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ComicCreatorExists(comicCreator.ComicId))
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
            ViewData["ComicId"] = new SelectList(_context.Comics, "ComicId", "Title", comicCreator.ComicId);
            ViewData["CreatorId"] = new SelectList(_context.Creators, "CreatorId", "FullName", comicCreator.CreatorId);
            return View(comicCreator);
        }

        // GET: ComicCreators/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comicCreator = await _context.ComicCreators
                .Include(c => c.Comic)
                .Include(c => c.Creator)
                .FirstOrDefaultAsync(m => m.ComicId == id);
            if (comicCreator == null)
            {
                return NotFound();
            }

            return View(comicCreator);
        }

        // POST: ComicCreators/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var comicCreator = await _context.ComicCreators.FindAsync(id);
            if (comicCreator != null)
            {
                _context.ComicCreators.Remove(comicCreator);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ComicCreatorExists(int id)
        {
            return _context.ComicCreators.Any(e => e.ComicId == id);
        }
    }
}

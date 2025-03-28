﻿using System;
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
    public class ComicsController : Controller
    {
        private readonly ComicsContext _context;

        public ComicsController(ComicsContext context)
        {
            _context = context;
        }

        // GET: Comics
        public async Task<IActionResult> Index()
        {
            var comicsContext = _context.Comics.Include(c => c.Genre).Include(c => c.Publisher);
            return View(await comicsContext.ToListAsync());
        }

        // GET: Comics/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comic = await _context.Comics
                .Include(c => c.Genre)
                .Include(c => c.Publisher)
                .FirstOrDefaultAsync(m => m.ComicId == id);
            if (comic == null)
            {
                return NotFound();
            }

            return View(comic);
        }

        // GET: Comics/Create
        public IActionResult Create()
        {
            ViewData["GenreId"] = new SelectList(_context.Genres, "GenreId", "GenreName");
            ViewData["PublisherId"] = new SelectList(_context.Publishers, "PublisherId", "Name");
            return View();
        }

        // POST: Comics/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
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

        // GET: Comics/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comic = await _context.Comics.FindAsync(id);
            if (comic == null)
            {
                return NotFound();
            }
            ViewData["GenreId"] = new SelectList(_context.Genres, "GenreId", "GenreName", comic.GenreId);
            ViewData["PublisherId"] = new SelectList(_context.Publishers, "PublisherId", "Name", comic.PublisherId);
            return View(comic);
        }

        // POST: Comics/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ComicId,Title,IssueNumber,ReleaseDate,PublisherId,GenreId,CoverImageUrl,Synopsis")] Comic comic)
        {
            if (id != comic.ComicId)
            {
                return NotFound();
            }

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
            ViewData["GenreId"] = new SelectList(_context.Genres, "GenreId", "GenreName", comic.GenreId);
            ViewData["PublisherId"] = new SelectList(_context.Publishers, "PublisherId", "Name", comic.PublisherId);
            return View(comic);
        }

        // GET: Comics/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comic = await _context.Comics
                .Include(c => c.Genre)
                .Include(c => c.Publisher)
                .FirstOrDefaultAsync(m => m.ComicId == id);
            if (comic == null)
            {
                return NotFound();
            }

            return View(comic);
        }

        // POST: Comics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var comic = await _context.Comics.FindAsync(id);
            if (comic != null)
            {
                _context.Comics.Remove(comic);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ComicExists(int id)
        {
            return _context.Comics.Any(e => e.ComicId == id);
        }
    }
}

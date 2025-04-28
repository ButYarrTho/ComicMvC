using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using ComicMvC.Data;

namespace ComicMvC.Controllers
{
    [Authorize(Policy = "RegisteredOnly")]
    public class CartController : Controller
    {
        private readonly ComicsContext _context;

        public CartController(ComicsContext context)
        {
            _context = context;
        }

        // GET: /Cart
        public IActionResult Index()
        {
            var ids = HttpContext.Session.GetObjectFromJson<List<int>>("CartItems")
                      ?? new List<int>();

            // Load the selected comics from the database
            var items = _context.Comics
                                .Where(c => ids.Contains(c.ComicId))
                                .ToList();

            return View(items);
        }

        // POST: /Cart/Add/5
        [HttpPost]
        public IActionResult Add(int id)
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<int>>("CartItems")
                       ?? new List<int>();

            if (!cart.Contains(id))
            {
                cart.Add(id);
                HttpContext.Session.SetObjectAsJson("CartItems", cart);
            }

            HttpContext.Session.SetInt32("CartCount", cart.Count);
            return RedirectToAction("Index", "Shopping");
        }

        // POST: /Cart/Clear
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Clear()
        {
            HttpContext.Session.Remove("CartItems");
            HttpContext.Session.Remove("CartCount");
            return RedirectToAction("Index", "Shopping");
        }
    }
}

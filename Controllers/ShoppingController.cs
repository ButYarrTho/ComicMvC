using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ComicMvC.Data;
using Microsoft.AspNetCore.Authorization;

namespace ComicMvC.Controllers
{
    public class ShoppingController : Controller
    {
        private readonly ComicsContext _context;

        public ShoppingController(ComicsContext context)
        {
            _context = context;
        }

        // Anyone can view the shopping page
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var comics = await _context.Comics.ToListAsync();
            return View(comics);
        }
    }
}

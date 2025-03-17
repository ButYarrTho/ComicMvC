    using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ComicMvC.Data;
namespace ComicMvC.Controllers
{
    public class ShoppingController : Controller
    {
        private readonly ComicsContext _context;

        public ShoppingController(ComicsContext context)
        {
            _context = context;
        }

        // GET: /Shopping
        public async Task<IActionResult> Index()
        {
            var comics = await _context.Comics.ToListAsync();
            return View(comics);
        }
    }
}
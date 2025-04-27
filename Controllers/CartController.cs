using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace ComicMvC.Controllers
{
    public class CartController : Controller
    {
        // POST: /Cart/Add/5
        [HttpPost]
        public IActionResult Add(int id)
        {
            // bump the count in session
            int count = HttpContext.Session.GetInt32("CartCount") ?? 0;
            HttpContext.Session.SetInt32("CartCount", ++count);

            // redirect back to shopping page
            return RedirectToAction("Index", "Shopping");
        }
    }
}

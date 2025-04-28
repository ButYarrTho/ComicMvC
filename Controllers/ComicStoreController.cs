using Microsoft.AspNetCore.Mvc;

namespace ComicMvC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComicStoreController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetComicStores()
        {
            var stores = new List<object>
            {
                new {
                    name = "Big Bang Comics",
                    address = "Dundrum, Dublin, Ireland",
                    latitude = 53.2901,
                    longitude = -6.2419
                },
                new {
                    name = "Sub-City Comics",
                    address = "Dublin City Centre, Ireland",
                    latitude = 53.3438,
                    longitude = -6.2647
                },
                new {
                    name = "Forbidden Planet Dublin",
                    address = "Dublin City, Ireland",
                    latitude = 53.3480,
                    longitude = -6.2603
                }
            };

            return Ok(stores);
        }
    }
}

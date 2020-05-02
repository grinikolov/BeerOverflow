using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace BeerOverflowAPI.Controllers
{
    public class UsersController : Controller
    {

        private readonly IUsersService _service;

        public UsersController(IUsersService service)
        {
            this._service = service ?? throw new ArgumentNullException(nameof(service));
        }

        public IActionResult Index()
        {
            return View();
        }
       // [HttpPost, ActionName("Delete")]
        [HttpPut, ActionName("Drink")]
        public async Task<IActionResult> Drink([FromQuery] int id, [FromQuery] int beerID) // was [FromBody] BeerDTO beerDTO)
        {
            if (id <= 0 || beerID == default)
            {
                return BadRequest();
            }

            try
            {
                var model = await this._service.Drink(id, beerID);
                return RedirectToAction("Details","Beers",beerID);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

    }
}
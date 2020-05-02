using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace BeerOverflowAPI.Controllers
{
    [Authorize]
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
        //[Route("{userID:int}")]
        //[HttpGet]//, ActionName("Drink")
        public async Task<IActionResult> Drink(int userID, int beerID) // was [FromBody] BeerDTO beerDTO)
        {
            if (userID <= 0 || beerID == default)
            {
                return BadRequest();
            }

            try
            {
                var model = await this._service.Drink(userID, beerID);
                return RedirectToAction("Details", "Beers", beerID);
                //return NotFound();
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

    }
}
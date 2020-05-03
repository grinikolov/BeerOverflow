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
        public async Task<IActionResult> Drink(int userID, int beerID)
        {
            if (userID <= 0 || beerID == default)
            {
                return BadRequest();
            }

            try
            {
                var model = await this._service.Drink(userID, beerID);
                return RedirectToAction("Details", "Beers", new { id = beerID });
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        public async Task<IActionResult> Wish(int userID, int beerID)
        {
            if (userID <= 0 || beerID == default)
            {
                return BadRequest();
            }

            try
            {
                var model = await this._service.Wish(userID, beerID);
                return RedirectToAction("Details", "Beers", new { id = beerID });
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        public async Task<IActionResult> Like(int userID, int reviewID)
        {
            if (userID <= 0 || reviewID == default)
            {
                return BadRequest();
            }

            try
            {
                var result = await this._service.Like(userID, reviewID);
                return RedirectToAction("Details", "Review", new { id = reviewID });


            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        public async Task<IActionResult> Flag(int userID, int reviewID)
        {
            if (userID <= 0 || reviewID == default)
            {
                return BadRequest();
            }

            try
            {
                var result = await this._service.Flag(userID, reviewID);
                return RedirectToAction("Details", "Review", new { id = reviewID });
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }
        public async Task<IActionResult> Rate(int userID, int beerID, int rating)
        {

            if (rating <= 0 || beerID == default || userID ==default )
            {
                return BadRequest();
            }

            try
            {
                var model = await this._service.Rate(userID, beerID, rating);
                return RedirectToAction("Details", "Beers", new { id = beerID });
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

    }
}
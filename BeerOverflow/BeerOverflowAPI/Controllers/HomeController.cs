using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BeerOverflowAPI.Models;
using Services.Contracts;
using BeerOverflowAPI.ViewMappers;

namespace BeerOverflowAPI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBeerService _beersService;

        public HomeController(ILogger<HomeController> logger, IBeerService beerService)
        {
            _logger = logger;
            this._beersService = beerService;
        }

        public IActionResult Index()
        {
            var randomBeer = this._beersService.GetRandom().Result.MapBeerDTOToView();
            return View(randomBeer);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

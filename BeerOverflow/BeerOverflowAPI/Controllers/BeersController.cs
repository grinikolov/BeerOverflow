using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BeerOverflow.Models;
using Database;
using Services.Contracts;
using BeerOverflowAPI.ViewMappers;
using BeerOverflowAPI.Models;
using System.Security.Claims;
using Services;
using Services.DTOs;

namespace BeerOverflowAPI.Controllers
{
    public class BeersController : Controller
    {
        private readonly IBeerService _service;
        private readonly IUsersService _usersService;
        private readonly IBreweryService _breweryService;
        private readonly ICountriesService _countriesService;
        private readonly IBeerStylesService _beerStylesService;
        public BeersController(IBeerService service,
                            IUsersService usersService,
                            IBreweryService breweryService,
                            ICountriesService countriesService,
            IBeerStylesService _beerStylesService)
        {
            this._service = service ?? throw new ArgumentNullException(nameof(service));
            this._usersService = usersService ?? throw new ArgumentNullException(nameof(service)); ;
            this._breweryService = breweryService ?? throw new ArgumentNullException(nameof(service)); 
            this._countriesService = countriesService ?? throw new ArgumentNullException(nameof(service)); ;
            this._beerStylesService = _beerStylesService ?? throw new ArgumentNullException(nameof(service)); ;
        }



        // GET: Beers
        public async Task<IActionResult> Index(string searchString)
        {
            // TODO: Approach for Wishing beer:
            //var temp = this.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            //Console.WriteLine(temp);
            // _usersService.Wish (temp, beerID); // 

            IEnumerable<BeerDTO> beers = null;
            if (!String.IsNullOrEmpty(searchString))
            {
                beers = await _service.Search(searchString);
            }
            else
            {
                beers = await _service.GetAllAsync();
            }

            var beersDTO = beers.Select(b => b.MapBeerDTOToView());

            return View(beersDTO);
        }

        // GET: Beers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var beer = await this._service.GetAsync(id);
            if (beer == null)
            {
                return NotFound();
            }

            return View(beer.MapBeerDTOToView());
        }

        // GET: Beers/Create
        public IActionResult Create()
        {
            ViewData["BreweryID"] = new SelectList( this._breweryService.GetAllAsync().Result, "ID", "Name");
            ViewData["CountryID"] = new SelectList(this._countriesService.GetAllAsync().Result, "ID", "Name");
            ViewData["StyleID"] = new SelectList(this._beerStylesService.GetAllAsync().Result, "ID", "Name");
            return View();
        }
        
        // POST: Beers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,ABV,StyleID,CountryID,BreweryID,Rating")] BeerViewModel beer)
        {
            if (ModelState.IsValid)
            {
                var theNewBeer = await this._service.CreateAsync(beer.MapBeerViewToDTO());
                beer = theNewBeer.MapBeerDTOToView();
                return RedirectToAction(nameof(Index));
            }

            ViewData["BreweryID"] = new SelectList(this._breweryService.GetAllAsync().Result, "ID", "Name");
            ViewData["CountryID"] = new SelectList(this._countriesService.GetAllAsync().Result, "ID", "Name");
            ViewData["StyleID"] = new SelectList(this._beerStylesService.GetAllAsync().Result, "ID", "Name");
            return View(beer);
        }
        /*
        // GET: Beers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var beer = await _context.Beers.FindAsync(id);
            if (beer == null)
            {
                return NotFound();
            }
            ViewData["BreweryID"] = new SelectList(_context.Breweries, "ID", "Name", beer.BreweryID);
            ViewData["CountryID"] = new SelectList(_context.Countries, "ID", "Name", beer.CountryID);
            ViewData["StyleID"] = new SelectList(_context.BeerStyles, "ID", "Description", beer.StyleID);
            return View(beer);
        }
        
        // POST: Beers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("ID,Name,ABV,StyleID,CountryID,BreweryID,Rating,CreatedOn,ModifiedOn,DeletedOn,IsDeleted")] Beer beer)
        {
            if (id != beer.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(beer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BeerExists(beer.ID))
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
            ViewData["BreweryID"] = new SelectList(_context.Breweries, "ID", "Name", beer.BreweryID);
            ViewData["CountryID"] = new SelectList(_context.Countries, "ID", "Name", beer.CountryID);
            ViewData["StyleID"] = new SelectList(_context.BeerStyles, "ID", "Description", beer.StyleID);
            return View(beer);
        }
        */

        // GET: Beers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var beer = await this._service.GetAsync(id);
            if (beer == null)
            {
                return NotFound();
            }

            return View(beer.MapBeerDTOToView());
        }

        // POST: Beers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var result = await this._service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }


    }
}

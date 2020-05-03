using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BeerOverflow.Models;
using Database;
using Services;
using BeerOverflowAPI.ViewMappers;
using BeerOverflowAPI.Models;
using Services.Mappers;
using Services.Contracts;
using Microsoft.AspNetCore.Authorization;

namespace BeerOverflowAPI.Controllers
{
    public class BreweryController : Controller
    {
        private readonly IBreweryService _service;
        private readonly ICountriesService _countryService;

        public BreweryController(IBreweryService service, ICountriesService _countryService)
        {
            this._service = service ?? throw new ArgumentNullException(nameof(service)); ;
            this._countryService = _countryService ?? throw new ArgumentNullException(nameof(service));
        }

        // GET: Breweries
        public async Task<IActionResult> Index()
        {
            var index = await _service.GetAllAsync();
            return View(index.Select(b => b.MapBreweryDTOToView()));
        }

        // GET: Breweries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var brewery = await _service.GetAsync(id);
            if (brewery == null)
            {
                return NotFound();
            }

            return View(brewery.MapBreweryDTOToView());
        }

        // GET: Breweries/Create
        [Authorize]
        public IActionResult Create()
        {
            ViewData["CountryID"] = new SelectList(this._countryService.GetAllAsync().Result, "ID", "Name");
            return View();
        }

        // POST: Breweries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("ID,Name,CountryID")] BreweryViewModel brewery)
        {
            if (ModelState.IsValid)
            {
                var newBrewery = await _service.CreateAsync(brewery.MapBreweryViewToDTO());
                //_context.Add(brewery);
                //await _context.SaveChangesAsync();
                brewery = newBrewery.MapBreweryDTOToView();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CountryID"] = new SelectList(this._countryService.GetAllAsync().Result, "ID", "Name", brewery.Name);
            return View(brewery);
        }

        // GET: Breweries/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var brewery = await _service.GetAsync(id);
            if (brewery == null)
            {
                return NotFound();
            }
            ViewData["CountryID"] = new SelectList(this._countryService.GetAllAsync().Result, "ID", "Name", brewery.CountryID);
            return View(brewery.MapBreweryDTOToView());
        }

        // POST: Breweries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,CountryID,IsDeleted")] BreweryViewModel brewery)
        {
            if (id != brewery.ID)
            {
                return NotFound();
            }
            var updateModel = brewery.MapBreweryViewToDTO();
            if (ModelState.IsValid)
            {
                try
                {
                    await _service.UpdateAsync(id, updateModel);
                }
                catch (Exception)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CountryID"] = new SelectList(this._countryService.GetAllAsync().Result, "ID", "Name", brewery.CountryID);
            return View(brewery);
        }

        // GET: Breweries/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var brewery = await _service.GetAsync(id);
            if (brewery == null)
            {
                return NotFound();
            }

            return View(brewery.MapBreweryDTOToView());
        }

        // POST: Breweries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var brewery = await _service.GetAsync(id);
            bool result;
            try
            {
                result = await _service.DeleteAsync(id);
            }
            catch (Exception)
            {

                return NotFound();
            }
            if (result)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return BadRequest();
            }
        }


        [Authorize]
        public async Task<IActionResult> Recover(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var brewery = await _service.GetAsync(id);
            if (brewery == null)
            {
                return NotFound();
            }
            return View(brewery.MapBreweryDTOToView());
        }

        [HttpPost, ActionName("Recover")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> RecoverConfirmed(int id)
        {
            try
            {
                var brewery = await _service.GetAsync(id);
                await _service.CreateAsync(brewery);
            }
            catch (Exception)
            {
                return BadRequest();
            }
            return RedirectToAction("Index");
        }
    }
}

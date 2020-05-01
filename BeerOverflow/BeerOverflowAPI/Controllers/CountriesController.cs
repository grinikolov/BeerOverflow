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
using Services.Mappers;
using BeerOverflowAPI.Models;
using Services.Contracts;

namespace BeerOverflowAPI.Controllers
{
    public class CountriesController : Controller
    {
        private readonly ICountriesService _service;

        public CountriesController(ICountriesService _service)
        {
            this._service = _service ?? throw new ArgumentNullException("No service");
        }

        // GET: Countries
        public async Task<IActionResult> Index()
        {
            var index = await _service.GetAllAsync();
            return View(index.Select(c => c.MapCountryDTOToView()));
        }

        // GET: Countries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var details = await _service.GetAsync(id);
            var country = details.MapCountryDTOToView();
            if (country == null)
            {
                return NotFound();
            }

            return View(country);
        }

        //// GET: Countries/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Countries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name")] CountryViewModel country)
        {
            if (ModelState.IsValid)
            {
                var returnCountry = await _service.CreateAsync(country.MapCountryViewToDTO());
                country = returnCountry.MapCountryDTOToView();
                return RedirectToAction(nameof(Index));
            }
            return View(country);
        }

        // GET: Countries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var country = await _service.GetAsync(id);
            if (country == null)
            {
                return NotFound();
            }
            return View(country.MapCountryDTOToView());
        }

        // POST: Countries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Breweries")] CountryViewModel country)
        {
            if (id != country.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _service.UpdateAsync(id, country.MapCountryViewToDTO());
                return RedirectToAction(nameof(Index));
            }
            return View(country);
        }

        // GET: Countries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var country = await _service.GetAsync(id);
            if (country == null)
            {
                return NotFound();
            }

            return View(country.MapCountryDTOToView());
        }

        // POST: Countries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (await _service.DeleteAsync(id))
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return BadRequest();
            }

        }

        public async Task<IActionResult> Recover(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var country = await _service.GetAsync(id);
            if (country == null)
            {
                return NotFound();
            }

            return View(country.MapCountryDTOToView());
        }

        // POST: Countries/Recover/5
        [HttpPost, ActionName("Recover")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RecoverConfirmed(int id)
        {
            try
            {
                var country = await _service.GetAsync(id);
                await _service.CreateAsync(country);
            }
            catch (Exception)
            {
                return BadRequest();
            }
            return RedirectToAction("Index");
        }
    }
}

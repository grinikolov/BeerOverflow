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
//using BeerOverflowAPI.ViewMappers;

namespace BeerOverflowAPI.Controllers
{
    //[Route("Country")]
    public class CountriesController : Controller
    {
        private readonly BOContext _context;

        public CountriesController(BOContext context)
        {
            _context = context;
        }

        // GET: Countries
        public async Task<IActionResult> Index()
        {
            var index = await new CountriesService(this._context).GetAll();
            return View(index.Select(c => c.MapCountryDTOToView()));
        }

        // GET: Countries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var details = await new CountriesService(this._context).GetAsync(id);
            var country = details.MapCountryDTOToView();
            //var country = await _context.Countries
            //    .FirstOrDefaultAsync(m => m.ID == id);
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
                var returnCountry = await new CountriesService(this._context).CreateAsync(country.MapCountryViewToDTO());
                //country.MapCountryViewToDTO();
                //country.CreatedOn = DateTime.UtcNow;
                //_context.Add(country);
                //await _context.SaveChangesAsync();
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
            var country = await new CountriesService(this._context).GetAsync(id);
            //var country = await _context.Countries.Include(c => c.Breweries).FirstOrDefaultAsync(c => c.ID == id);
            if (country == null)
            {
                return NotFound();
            }
            return View(new CountryViewModel() { 
                ID = country.ID, 
                Name = country.Name,
                Breweries = country.Breweries.Select(b => b.Name).ToList()
            });
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
                await new CountriesService(this._context).UpdateAsync(country.ID, country.MapCountryViewToDTO());
                //try
                //{
                //    country.ModifiedOn = DateTime.UtcNow;
                //    _context.Update(country);
                //    await _context.SaveChangesAsync();
                //}
                //catch (DbUpdateConcurrencyException)
                //{
                //    if (!CountryExists(country.ID))
                //    {
                //        return NotFound();
                //    }
                //    else
                //    {
                //        return BadRequest();
                //    }
                //}
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
            var country = await new CountriesService(this._context).GetAsync(id);
            //var country = await _context.Countries
            //    .FirstOrDefaultAsync(m => m.ID == id);
            if (country == null)
            {
                return NotFound();
            }

            return View(new CountryViewModel()
            {
                ID = country.ID,
                Name = country.Name,
                Breweries = country.Breweries.Select(b => b.Name).ToList()
            });
        }

        // POST: Countries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (await new CountriesService(this._context).DeleteAsync(id))
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return BadRequest();
            }
            //var country = await _context.Countries.FindAsync(id);
            //country.DeletedOn = DateTime.UtcNow;
            //country.IsDeleted = true;
            ////_context.Countries.Remove(country);
            //_context.Countries.Update(country);
            //await _context.SaveChangesAsync();
            
        }

        public async Task<IActionResult> Recover(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var country = await new CountriesService(this._context).GetAsync(id);
            //var country = await _context.Countries
            //    .FirstOrDefaultAsync(m => m.ID == id);
            if (country == null)
            {
                return NotFound();
            }

            return View(new CountryViewModel()
            {
                ID = country.ID,
                Name = country.Name,
                Breweries = country.Breweries.Select(b => b.Name).ToList()
            });
        }

        // POST: Countries/Delete/5
        [HttpPost, ActionName("Recover")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RecoverConfirmed(int id)
        {
            if (await new CountriesService(this._context).RecoverAsync(id))
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return BadRequest();
            }
            
            //var country = await _context.Countries.FindAsync(id);
            //country.DeletedOn = null;
            //country.IsDeleted = false;
            ////_context.Countries.Remove(country);
            //_context.Countries.Update(country);
            //await _context.SaveChangesAsync();
            
        }

        //private bool CountryExists(int id)
        //{
        //    return _context.Countries.Any(e => e.ID == id);
        //}
    }
}

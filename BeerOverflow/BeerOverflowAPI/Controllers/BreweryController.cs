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

namespace BeerOverflowAPI.Controllers
{
    public class BreweryController : Controller
    {
        private readonly BOContext _context;

        public BreweryController(BOContext context)
        {
            _context = context;
        }

        // GET: Breweries
        public async Task<IActionResult> Index()
        {
            var index = await new BreweryServices(this._context).GetAll();
            //var bOContext = _context.Breweries.Include(b => b.Country);
            //return View(await bOContext.ToListAsync());
            return View(index.Select(b => b.MapBreweryDTOToView()));
        }

        // GET: Breweries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var brewery = await new BreweryServices(this._context).GetBrewery(id);
            //var brewery = await _context.Breweries
            //    .Include(b => b.Country)
            //    .FirstOrDefaultAsync(m => m.ID == id);
            if (brewery == null)
            {
                return NotFound();
            }

            return View(brewery.MapBreweryDTOToView());
        }

        // GET: Breweries/Create
        public IActionResult Create()
        {
            ViewData["CountryID"] = new SelectList(_context.Countries, "ID", "Name");
            return View();
        }

        // POST: Breweries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,CountryID"/*,CreatedOn,ModifiedOn,DeletedOn,IsDeleted"*/)] BreweryViewModel brewery)
        {
            if (ModelState.IsValid)
            {
                var newBrewery = await new BreweryServices(this._context).Create(brewery.MapBreweryViewToDTO());
                //_context.Add(brewery);
                //await _context.SaveChangesAsync();
                brewery = newBrewery.MapBreweryDTOToView();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CountryID"] = new SelectList(_context.Countries, "ID", "Name", brewery.Name);
            return View(brewery);
        }

        // GET: Breweries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var brewery = await _context.Breweries.FindAsync(id);
            if (brewery == null)
            {
                return NotFound();
            }
            ViewData["CountryID"] = new SelectList(_context.Countries, "ID", "Name", brewery.CountryID);
            return View(brewery);
        }

        // POST: Breweries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,CountryID,CreatedOn,ModifiedOn,DeletedOn,IsDeleted")] Brewery brewery)
        {
            if (id != brewery.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(brewery);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BreweryExists(brewery.ID))
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
            ViewData["CountryID"] = new SelectList(_context.Countries, "ID", "Name", brewery.CountryID);
            return View(brewery);
        }

        // GET: Breweries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var brewery = await _context.Breweries
                .Include(b => b.Country)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (brewery == null)
            {
                return NotFound();
            }

            return View(brewery);
        }

        // POST: Breweries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var brewery = await _context.Breweries.FindAsync(id);
            _context.Breweries.Remove(brewery);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BreweryExists(int? id)
        {
            return _context.Breweries.Any(e => e.ID == id);
        }
    }
}

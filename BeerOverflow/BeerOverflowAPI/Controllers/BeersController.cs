using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BeerOverflow.Models;
using Database;

namespace BeerOverflowAPI.Controllers
{
    public class BeersController : Controller
    {
        private readonly BOContext _context;

        public BeersController(BOContext context)
        {
            _context = context;
        }

        // GET: Beers
        public async Task<IActionResult> Index()
        {
            var bOContext = _context.Beers.Include(b => b.Brewery).Include(b => b.Country).Include(b => b.Style);
            return View(await bOContext.ToListAsync());
        }

        // GET: Beers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var beer = await _context.Beers
                .Include(b => b.Brewery)
                .Include(b => b.Country)
                .Include(b => b.Style)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (beer == null)
            {
                return NotFound();
            }

            return View(beer);
        }

        // GET: Beers/Create
        public IActionResult Create()
        {
            ViewData["BreweryID"] = new SelectList(_context.Breweries, "ID", "Name");
            ViewData["CountryID"] = new SelectList(_context.Countries, "ID", "Name");
            ViewData["StyleID"] = new SelectList(_context.BeerStyles, "ID", "Description");
            return View();
        }

        // POST: Beers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,ABV,StyleID,CountryID,BreweryID,Rating,CreatedOn,ModifiedOn,DeletedOn,IsDeleted")] Beer beer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(beer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BreweryID"] = new SelectList(_context.Breweries, "ID", "Name", beer.BreweryID);
            ViewData["CountryID"] = new SelectList(_context.Countries, "ID", "Name", beer.CountryID);
            ViewData["StyleID"] = new SelectList(_context.BeerStyles, "ID", "Description", beer.StyleID);
            return View(beer);
        }

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

        // GET: Beers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var beer = await _context.Beers
                .Include(b => b.Brewery)
                .Include(b => b.Country)
                .Include(b => b.Style)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (beer == null)
            {
                return NotFound();
            }

            return View(beer);
        }

        // POST: Beers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var beer = await _context.Beers.FindAsync(id);
            _context.Beers.Remove(beer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BeerExists(int? id)
        {
            return _context.Beers.Any(e => e.ID == id);
        }
    }
}

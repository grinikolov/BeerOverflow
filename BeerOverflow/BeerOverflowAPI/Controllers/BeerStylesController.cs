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
using BeerOverflowAPI.Models;
using Services;
using BeerOverflowAPI.ViewMappers;

namespace BeerOverflowAPI.Controllers
{
    public class BeerStylesController : Controller
    {
        private readonly IBeerStylesService _service;

        public BeerStylesController(IBeerStylesService service)
        {
            this._service = service ?? throw new ArgumentNullException(nameof(service));
        }

        // GET: BeerStyles
        public async Task<IActionResult> Index()
        {
            var styles = this._service.GetAllAsync()
                .Result.Select(s => s.MapDTOToView());

            return View(styles);
        }

        // GET: BeerStyles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var beerStyle = await this._service.GetAsync(id);

            if (beerStyle == null)
            {
                return NotFound();
            }

            return View(beerStyle.MapDTOToView());
        }

        // GET: BeerStyles/Create
        public IActionResult Create()
        {
            return View();
        }
        // POST: BeerStyles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description")] BeerStyleViewModel beerStyle)
        {
            if (ModelState.IsValid)
            {
                var beerStyleDTO = beerStyle.MapViewToDTO();
                await this._service.CreateAsync(beerStyleDTO);
                return RedirectToAction(nameof(Index));
            }
            return View(beerStyle);
        }

        // GET: BeerStyles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var beerStyle = await this._service.GetAsync(id);
            if (beerStyle == null)
            {
                return NotFound();
            }
            return View(beerStyle.MapDTOToView());
        }

        // POST: BeerStyles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("ID,Name,Description")] BeerStyleViewModel beerStyle)
        {
            if (id != beerStyle.ID)
            {
                return NotFound();
            }

                var returnModel = await this._service.UpdateAsync(id, beerStyle.MapViewToDTO());
                if (returnModel == null)
                {
                    return NotFound();
                }

                
            
            return View(returnModel.MapDTOToView());
        }

        // GET: BeerStyles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var beerStyle = await this._service.GetAsync(id);
            if (beerStyle == null)
            {
                return NotFound();
            }

            return View(beerStyle.MapDTOToView());
        }

        // POST: BeerStyles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var beerStyle = await this._service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

    }
}

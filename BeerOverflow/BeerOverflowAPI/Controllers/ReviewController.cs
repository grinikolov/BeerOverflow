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
using Services.Contracts;
using BeerOverflowAPI.ViewMappers;
using BeerOverflowAPI.Models;

namespace BeerOverflowAPI.Controllers
{
    public class ReviewController : Controller
    {
        private readonly IReviewsService _service;
        private readonly IBeerService _beerService;
        private readonly IUsersService _userService;

        public ReviewController(IReviewsService _service, IBeerService _beerService, IUsersService _usersService)
        {
            this._service = _service ?? throw new ArgumentNullException("Service not found");
            this._beerService = _beerService ?? throw new ArgumentNullException("Service not found");
            this._userService = _userService ?? throw new ArgumentNullException("Service not found");
        }

        // GET: Review
        public async Task<IActionResult> Index(int id)
        {
            var reviews = await _service.GetAllByBeerAsync(id);
            return View(reviews.Select(r => r.MapDTOToReviewView()));
        }

        // GET: Review/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var review = await _service.GetAsync(id);
            if (review == null)
            {
                return NotFound();
            }

            return View(review.MapDTOToReviewView());
        }

        // GET: Review/Create
        public IActionResult Create()
        {
            ViewData["BeerID"] = new SelectList(_beerService.GetAllAsync().Result, "ID", "Name");
            ViewData["UserID"] = new SelectList(_userService.GetAllUsers().Result, "Id", "Name");
            return View();
        }

        // POST: Review/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,BeerID,UserID,Rating,Description,LikesCount,IsDeleted,IsFlagged")] ReviewViewModel review)
        {
            if (ModelState.IsValid)
            {
                await _service.CreateAsync(review.MapReviewViewToDTO());
                return RedirectToAction(nameof(Details),"BeersController",review.BeerID);
            }
            ViewData["BeerID"] = new SelectList(_beerService.GetAllAsync().Result, "ID", "Name");
            ViewData["UserID"] = new SelectList(_userService.GetAllUsers().Result, "Id", "Name");
            return View(review);
        }

        // GET: Review/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var review = await _service.GetAsync(id);
            if (review == null)
            {
                return NotFound();
            }
            ViewData["BeerID"] = new SelectList(_beerService.GetAllAsync().Result, "ID", "Name");
            ViewData["UserID"] = new SelectList(_userService.GetAllUsers().Result, "Id", "Name");
            return View(review.MapDTOToReviewView());
        }

        // POST: Review/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("ID,BeerID,UserID,Rating,Description,LikesCount,IsDeleted,IsFlagged")] ReviewViewModel review)
        {
            if (id != review.ID)
            {
                return NotFound();
            }
            
            if (ModelState.IsValid)
            {
                try
                {
                    var updateModel = review.MapReviewViewToDTO();
                    await _service.UpdateAsync(id, updateModel);
                }
                catch (Exception)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Details), "BeersController", review.BeerID);
            }
            ViewData["BeerID"] = new SelectList(_beerService.GetAllAsync().Result, "ID", "Name");
            ViewData["UserID"] = new SelectList(_userService.GetAllUsers().Result, "Id", "Name");
            return View(review);
        }

        // GET: Review/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var review = await _service.GetAsync(id);
            if (review == null)
            {
                return NotFound();
            }

            return View(review);
        }

        // POST: Review/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var review = await _service.GetAsync(id);
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
                return RedirectToAction(nameof(Details), "BeersController", review.BeerID);
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
            var review = await _service.GetAsync(id);
            if (review == null)
            {
                return NotFound();
            }
            return View(review.MapDTOToReviewView());
        }

        [HttpPost, ActionName("Recover")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RecoverConfirmed(int id)
        {
            try
            {
                var review = await _service.GetAsync(id);
                await _service.CreateAsync(review);
                return RedirectToAction(nameof(Details), "BeersController", review.BeerID);
            }
            catch (Exception)
            {
                return BadRequest();
            } 
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeerOverflowAPI.Models;
using BeerOverflowAPI.ViewMappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.Contracts;

namespace BeerOverflowAPI.Controllers
{
    [Authorize]
    public class CommentsController : Controller
    {
        private readonly ICommentService _service;
        private readonly IReviewsService _reviewService;
        //private readonly IBeerService _beerService;
        private readonly IUsersService _userService;

        public CommentsController(IReviewsService _reviewService, IUsersService _usersService, ICommentService _service)
        {
            this._service = _service ?? throw new ArgumentNullException("Service not found");
            this._reviewService = _reviewService ?? throw new ArgumentNullException("Service not found");
            this._userService = _usersService ?? throw new ArgumentNullException("Service not found");
        }
        //public IActionResult Index()
        //{
        //    return View();
        //}

        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var comment = await _service.GetAsync(id);
        //    if (comment == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(comment.MapDTOToCommentView());
        //}

        public IActionResult Create(int userID, int reviewID)
        {
            ViewData["BeerID"] = _reviewService.GetAsync(reviewID);
            ViewData["UserID"] = _userService.GetUser(userID);
            return View();
        }

        // POST: Review/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,ReviewID,UserID,Description,IsDeleted")] CommentViewModel comment)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _service.CreateAsync(comment.MapCommentViewToDTO());
                    return RedirectToAction("Details", "Review", new { id = comment.ReviewID });
                }
                catch (Exception)
                {

                    return RedirectToAction("Index", "Home");
                }
                
            }
            ViewData["Review"] = _reviewService.GetAsync(comment.ReviewID);
            ViewData["UserID"] = _userService.GetUser(comment.UserID);
            return View(comment);
        }
    }
}
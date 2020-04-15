using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BeerOverflow.Models;
using Database;
using Services;
using Services.DTOs;

namespace BeerOverflowAPI.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewsService _service;

        public ReviewsController(IReviewsService service)
        {
            this._service = service;
        }

        // GET: api/Reviews
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Review>>> GetReviews()
        {
            try
            {
                var reviews = await this._service.GetAllReviews();
                return Ok(reviews);
            }
            catch (Exception)
            {
                return NoContent();
            }
        }

        // GET: api/Reviews/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Review>> GetReview(int id)
        {
            var review = await this._service.GetReview(id);
            if (review == null)
            {
                return NotFound();
            }

            return Ok(review);
        }

        // PUT: api/Reviews/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReview(int id, ReviewDTO review)
        {
            if (id <= 0 || review == null)
            {
                return BadRequest();
            }

            var model = await this._service.UpdateReview(id, review);
            return Ok(model);
        }

        // POST: api/Reviews
        [HttpPost]
        public async Task<ActionResult<ReviewDTO>> PostReview(ReviewDTO review)
        {
            var theNewReview = await this._service.CreateReview(review);
            return CreatedAtAction("GetReview", theNewReview);
        }

        // DELETE: api/Reviews/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteReview(int id)
        {
            return await this._service.DeleteReview(id);
        }

    }
}

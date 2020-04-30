using BeerOverflow.Models;
using Database;
using Microsoft.EntityFrameworkCore;
using Services.DTOs;
using Services.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ReviewsService : IReviewsService
    {
        private readonly BOContext _context;

        public ReviewsService(BOContext context)
        {
            this._context = context;
        }


        /// <summary>
        /// Get all reviews on record
        /// </summary>
        /// <returns>Returns a modified list of reviews on record</returns>
        public async Task<IEnumerable<ReviewDTO>> GetAllAsync()
        {
            var reviews = await this._context.Reviews
                .Include(r => r.Beer)
                .Include(r => r.User)
                .ToListAsync();
            var reviewsDto = reviews.Select(r => r.MapReviewToDTO()).ToList();
            if (reviewsDto.Any(c => c.Beer == null || c.User == null))
            {
                return null;
            }
            return reviewsDto;
        }
        /// <summary>
        /// Gets a review by ID
        /// </summary>
        /// <param name="id">Id of review</param>
        /// <returns>Returns a modified specific review on record</returns>
        public async Task<ReviewDTO> GetAsync(int? id)
        {
            var review = await this._context.Reviews
                    .Include(r => r.Beer)
                    .Include(r => r.User)
                    .FirstOrDefaultAsync(r => r.ID == id);

            if (review == null)
            {
                return null;
            }
            var model = review.MapReviewToDTO();
            if (model.BeerID == null || model.UserID == null)
            {
                return null;
            }
            return model;
        }

        /// <summary>
        /// Creates a review and writes it to the database.
        /// </summary>
        /// <param name="model">Input ReviewDTO object</param>
        /// <returns>Returns the reevaluated input object</returns>
        public async Task<ReviewDTO> CreateAsync(ReviewDTO model)
        {
            var review = model.MapDTOToReview();
            if (review.Beer == null || review.User == null)
            {
                return null;
            }
            #region Check if exists
            var theReview = await _context.Reviews
                .Include(r => r.Beer)
                .Include(r => r.User)
                .FirstOrDefaultAsync(r => r.Beer.Name == model.Beer.Name || r.User.Name == model.User.Name);
            if (theReview == null)
            {
                var theBeer = await this._context.Beers.FirstOrDefaultAsync(b => b.Name == model.Beer.Name);
                var theUser = await this._context.Users.FirstOrDefaultAsync(u => u.Name == model.User.Name);
                review.Beer = theBeer;
                review.User = theUser;
                review.CreatedOn = DateTime.UtcNow;
                this._context.Reviews.Add(review);
                await this._context.SaveChangesAsync();
            }
            else if (theReview.User.IsDeleted == false && theReview.Beer.IsDeleted == false)
            {
                theReview.IsDeleted = false;
                theReview.DeletedOn = null;
                theReview.ModifiedOn = DateTime.UtcNow;
                _context.Reviews.Update(theReview);
                var commentsOfReview = await _context.Comments
                                                .Include(c => c.User)
                                                .Include(c => c.Review)
                                                .ToListAsync();
                foreach (var item in commentsOfReview)
                {
                    await new CommentService(_context).CreateAsync(item.MapCommentToDTO());
                }
                await this._context.SaveChangesAsync();
            }
            #endregion
            var beer = await _context.Beers.FirstOrDefaultAsync(b => b.Name == model.Beer.Name);
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Name == model.User.Name);
            var returnModel = await this._context.Reviews
                .FirstOrDefaultAsync(r => r.BeerID == beer.ID && r.UserID == user.ID);
            model.ID = returnModel.ID;
            return model;
        }

        /// <summary>
        /// Updates the Review
        /// </summary>
        /// <param name="id">ID of the Review to be updated.</param>
        /// <param name="model">Input object with update information.</param>
        /// <returns>Returns the reevaluated input object</returns>
        public async Task<ReviewDTO> UpdateAsync(int? id, ReviewDTO model)
        {
            var review = await _context.Reviews.FindAsync(id);
            if (review == null) return null;
            review.Description = model.Description;
            review.ModifiedOn = DateTime.UtcNow;
            this._context.Update(review);

            try
            {
                await this._context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReviewExists(id))
                {
                    return null;
                }
            }
            return model;
        }

        /// <summary>
        /// Deletes specified record of review
        /// </summary>
        /// <param name="id">Id of record</param>
        /// <returns>Bool</returns>
        public async Task<bool> DeleteAsync(int? id)
        {
            try
            {
                var review = await this._context.Reviews
                    .Include(r => r.User)
                    .Include(r => r.Beer)
                    .Include(r => r.Comments)
                    .FirstOrDefaultAsync(r => r.ID == id) ?? throw new ArgumentNullException();
                review.IsDeleted = true;
                review.DeletedOn = review.ModifiedOn = DateTime.UtcNow;
                this._context.Update(review);
                foreach (var item in review.Comments)
                {
                    await new CommentService(_context).DeleteAsync(item.ID);
                }
                await this._context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private bool ReviewExists(int? id)
        {
            return this._context.Reviews.Any(e => e.ID == id);
        }

    }
}

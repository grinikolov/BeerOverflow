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

        public async Task<IEnumerable<ReviewDTO>> GetAllReviews()
        {
            var reviews = await this._context.Reviews
                .Select(r => r.MapReviewToDTO())
                .ToListAsync();

            return reviews;
        }

        public async Task<ReviewDTO> GetReview(int id)
        {
            try
            {
                var review = await this._context.Reviews
                    .Where(r => r.IsDeleted == false)
                    .FirstOrDefaultAsync(r => r.ID == id) ?? throw new ArgumentNullException(); ;

                var model = review.MapReviewToDTO();

                return model;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ReviewDTO> UpdateReview(int id, ReviewDTO model)
        {
            if (id != model.ID)
            {
                throw new ArgumentException();
            }

            this._context.Entry(model).State = EntityState.Modified;

            try
            {
                await this._context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReviewExists(id))
                {
                    throw new ArgumentNullException("Review does not exist.");
                }
                else
                {
                    throw;
                }
            }

            return model;
        }

        public async Task<ReviewDTO> CreateReview(ReviewDTO model)
        {
            ReviewDTO modelToReturn;
            try
            {
                var theBeer = this._context.Beers.FirstOrDefault(b => b.Name == model.Beer.Name);
                var theUser = this._context.Users.FirstOrDefault(u => u.Name == model.User.Name);

                var review = new Review
                {
                    //ID = model.ID,
                    BeerID = theBeer.ID,
                    Beer = theBeer,
                    UserID = model.UserID,
                    User = theUser,
                    Rating = model.Rating,
                    Description = model.Description,
                    LikesCount = model.LikesCount,
                    Comments = new List<Comment>(),
                    IsDeleted = model.IsDeleted,
                    IsFlagged = model.IsFlagged,
                };

                this._context.Reviews.Add(review);
                await this._context.SaveChangesAsync();

                modelToReturn = review.MapReviewToDTO();
            }
            catch (Exception)
            {
                throw new Exception("Could not create Review in Service.");
            }
            return modelToReturn;
        }

        public async Task<bool> DeleteReview(int id)
        {
            try
            {
                var review = await this._context.Reviews.FindAsync(id) ?? throw new ArgumentNullException();
                review.IsDeleted = true;
                review.DeletedOn = DateTime.UtcNow;
                this._context.Update(review);
                //this._context.Reviews.Remove(review);
                await this._context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private bool ReviewExists(int id)
        {
            return this._context.Reviews.Any(e => e.ID == id);
        }
        //private ReviewDTO MapToDTO(Review review)
        //{
        //    var model = new ReviewDTO
        //    {
        //        ID = review.ID,
        //        BeerID = review.BeerID,
        //        Beer = new BeerDTO { ID = review.Beer.ID },
        //        UserID = review.UserID,
        //        User = new UserDTO { ID = review.User.ID },
        //        Rating = review.Rating,
        //        Description = review.Description,
        //        LikesCount = review.LikesCount,
        //        //Comments = review.Comments.Select(c => MapCommentToDTO(c)).ToList(),
        //        IsFlagged = review.IsFlagged,
        //    };
        //    return model;

        //}

        //private CommentDTO MapCommentToDTO(Comment c)
        //{
        //    var comment = new CommentDTO()
        //    {
        //        ID = c.ID,
        //        BeerID = c.BeerID,
        //        UserID = c.UserID,
        //        Description = c.Description,
        //        LikesCount = c.LikesCount,
        //    };
        //    return comment;
        //}





    }
}

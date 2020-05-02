using BeerOverflow.Models;
using Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Services.Mappers
{
    public static class ReviewMapper
    {
        public static ReviewDTO MapReviewToDTO(this Review review)
        {
            try
            {
                var model = new ReviewDTO
                {
                    ID = review.ID,
                    BeerID = review.BeerID,
                    Beer = new BeerDTO { ID = review.Beer.ID , Name = review.Beer.Name},
                    UserID = review.UserID,
                    User = new UserDTO { ID = review.User.IDOld , Name = review.User.Name},
                    Rating = review.Rating,
                    Description = review.Description,
                    LikesCount = review.LikesCount,
                    IsFlagged = review.IsFlagged,
                };
                if (review.Comments != null)
                {
                    model.Comments = review.Comments.Select(c => c.MapCommentToDTO()).ToList();
                }
                else
                {
                    model.Comments = null;
                }
                return model;
            }
            catch (Exception)
            {
                return new ReviewDTO();
            }
        }

        public static Review MapDTOToReview(this ReviewDTO model)
        {
            try
            {
                var review = new Review
                {
                    ID = model.ID,
                    //BeerID = model.BeerID,
                    //UserID = model.UserID,
                    Rating = model.Rating,
                    Description = model.Description,
                    //LikesCount = model.LikesCount,
                    IsFlagged = model.IsFlagged,
                };
                if (model.Comments != null)
                {
                    review.Comments = model.Comments.Select(r => r.MapDTOToComment()).ToList();
                }
                else
                {
                    review.Comments = new List<Comment>();
                }
                return review;
            }
            catch (Exception)
            {

                return new Review();
            }
        }
    }
}

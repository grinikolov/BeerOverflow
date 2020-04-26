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
            var model = new ReviewDTO
            {
                ID = review.ID,
                BeerID = review.BeerID,
                Beer = new BeerDTO { ID = review.Beer.ID },
                UserID = review.UserID,
                User = new UserDTO { ID = review.User.ID },
                Rating = review.Rating,
                Description = review.Description,
                LikesCount = review.LikesCount,
                Comments = review.Comments.Count>0 ?
                    review.Comments.Select(c => c.MapCommentToDTO())
                    .ToList() : null,
                IsFlagged = review.IsFlagged,
            };
            return model;
        }

        public static Review MapDTOToReview(this ReviewDTO model)
        {
            var review = new Review()
            {
                ////ID = model.ID,
                //BeerID = model.ID,
                //Beer = model.Beer,
                //UserID = model.UserID,
                //User = model.User,
                //Rating = model.Rating,
                //Description = model.Description,
                //LikesCount = model.LikesCount,
                //Comments = new List<Comment>(),
                //IsDeleted = model.IsDeleted,
                //IsFlagged = model.IsFlagged,
            };
            return review;
        }
    }
}

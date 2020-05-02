using BeerOverflowAPI.Models;
using Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeerOverflowAPI.ViewMappers
{
    public static class ReviewViewMapper
    {
        public static ReviewDTO MapReviewViewToDTO(this ReviewViewModel view)
        {
            var reviewDto = new ReviewDTO()
            {
                ID = view.ID,
                BeerID = view.BeerID,
                UserID = view.UserID,
                Rating = view.Rating,
                Description = view.Description,
                LikesCount = view.LikesCount,
                IsFlagged = view.IsFlagged
            };
            if (view.Comments != null)
            {
                reviewDto.Comments = view.Comments.Select(c => new CommentDTO() 
                { 
                    ID = c.ID, 
                    BeerID = c.BeerID, 
                    UserID = c.UserID, 
                    LikesCount = c.LikesCount,
                    Description = c.Description,
                }).ToList();
            }
            return reviewDto;
        }

        public static ReviewViewModel MapDTOToReviewView(this ReviewDTO dto)
        {
            var reviewDto = new ReviewViewModel()
            {
                ID = dto.ID,
                BeerID = dto.BeerID,
                UserID = dto.UserID,
                Rating = dto.Rating,
                Description = dto.Description,
                LikesCount = dto.LikesCount,
                IsFlagged = dto.IsFlagged
            };
            if (dto.Comments != null)
            {
                reviewDto.Comments = dto.Comments.Select(c => new CommentViewModel()
                {
                    ID = c.ID,
                    BeerID = c.BeerID,
                    UserID = c.UserID,
                    LikesCount = c.LikesCount,
                    Description = c.Description,
                }).ToList();
            }
            return reviewDto;
        }
    }
}

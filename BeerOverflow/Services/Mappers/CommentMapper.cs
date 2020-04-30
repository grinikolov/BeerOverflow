using BeerOverflow.Models;
using Services.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Mappers
{
    public static class CommentMapper
    {
        public static CommentDTO MapCommentToDTO(this Comment c)
        {
            try
            {
                var comment = new CommentDTO()
                {
                    ID = c.ID,
                    BeerID = c.BeerID,
                    UserID = c.UserID,
                    ReviewID = c.ReviewID,
                    Description = c.Description,
                    LikesCount = c.LikesCount,
                };
                return comment;
            }
            catch (Exception)
            {

                return new CommentDTO();
            }

        }

        public static Comment MapDTOToComment(this CommentDTO c)
        {
            try
            {
                var comment = new Comment()
                {
                    ID = c.ID,
                    BeerID = c.BeerID,
                    UserID = c.UserID,
                    ReviewID = c.ReviewID,
                    Description = c.Description,
                    LikesCount = c.LikesCount,
                };
                return comment;
            }
            catch (Exception)
            {

                return new Comment();
            }

        }
    }
}

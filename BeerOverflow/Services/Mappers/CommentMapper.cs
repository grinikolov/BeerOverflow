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
            var comment = new CommentDTO()
            {
                ID = c.ID,
                BeerID = c.BeerID,
                UserID = c.UserID,
                Description = c.Description,
                LikesCount = c.LikesCount,
            };
            return comment;
        }
    }
}

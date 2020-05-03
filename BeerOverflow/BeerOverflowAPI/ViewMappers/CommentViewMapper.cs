using BeerOverflowAPI.Models;
using Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeerOverflowAPI.ViewMappers
{
    public static class CommentViewMapper
    {
        public static CommentViewModel MapDTOToCommentView(this CommentDTO dto)
        {
            var commentView = new CommentViewModel()
            {
                    ID = dto.ID,
                    BeerID = dto.BeerID,
                    //BeerName = dto.Beer.Name,
                    UserID = dto.UserID,
                    UserName = dto.User.Name,
                    ReviewID = dto.ReviewID,
                    Description = dto.Description,
            };
            return commentView;
        }

        public static CommentDTO MapCommentViewToDTO(this CommentViewModel view)
        {
            var commentDTO = new CommentDTO()
            {
                ID = view.ID,
                BeerID = view.BeerID,
                UserID = view.UserID,
                ReviewID = view.ReviewID,
                Description = view.Description,
            };
            return commentDTO;
        }
    }
}

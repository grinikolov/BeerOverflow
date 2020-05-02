using BeerOverflow.Models;
using Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Services.Mappers
{
    public static class UserMapper
    {
        public static UserDTO MapUserToDTO(this User u)
        {
            var model = new UserDTO
            {
                ID = u.Id,
                Name = u.Name,
                Password = u.Password,
               
                //TODO: Map FlagList and Likes
                //FlagList
                //LikesList = u.LikesList.Count >0 ? u.LikesList(l => )
            };

            if (u.DrankLists != null)
            {
                model.DrankLists = u.DrankLists.Count > 0 ?
                     u.DrankLists.Select(x => new DrankListDTO()
                     {
                         BeerID = x.BeerID,
                         BeerName = x.Beer.Name,
                         UserID = x.UserID,
                         UserName = x.User.Name,
                     }).ToList() : null;
            }
            else
            {
                model.DrankLists = new List<DrankListDTO>();
            }
            if (u.WishLists != null)
            {
               model.WishLists = u.WishLists.Count > 0 ?
                    u.WishLists.Select(x => new WishListDTO()
                    {
                        BeerID = x.BeerID,
                        BeerName = x.Beer.Name,
                        UserID = x.UserID,
                        UserName = x.User.Name,
                    }).ToList() : null;
            }
            else
            {
                model.WishLists = new List<WishListDTO>();
            }
            if (u.ReviewList != null)
            {
                model.ReviewsList = u.ReviewList.Count > 0 ?
                    u.ReviewList.Select(r => r.MapReviewToDTO()
                    ).ToList() : null ;
            }
            else
            {
                model.ReviewsList = new List<ReviewDTO>();
            }
            if (u.CommentList != null)
            {
                model.CommentsList = u.CommentList.Count > 0 ?
                    u.CommentList.Select(c => c.MapCommentToDTO()
                    ).ToList() : null;
            }
            else
            {
                model.CommentsList = new List<CommentDTO>();
            }

            return model;
        }
        public static User MapToUser(this UserDTO model)
        {
            var theUser = new User()
            {
                Id = model.ID ?? default,
                Name = model.Name,
                Password = model.Password,
                //CreatedOn = DateTime.UtcNow,
                //DrankList = new List<Beer>(),
                //WishList = model.WishList.ToHashSet() ?? new HashSet<Beer>(),
                //ReviewList = model.ReviewsList, //new List<Review>(),
                //CommentList = model.CommentsList, //new List<Comment>(),
                //FlagList = model.FlagList,  //new List<Flag>(),
                //LikesList = model.LikesList, //new List<Like>(),
            };
            return theUser;
        }

    }
}

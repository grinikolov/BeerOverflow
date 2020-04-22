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
                ID = u.ID,
                Name = u.Name,
                Password = u.Password,
                DranksList = u.DranksList.Select(b => b.MapBeerToDTO()).ToList(),
                DrankLists = u.DrankLists.Select(dl => new DrankListDTO() 
                { 
                    BeerID = dl.BeerID,
                    Beer = dl.Beer.MapBeerToDTO(),
                    UserID = dl.UserID,
                    User = dl.User.MapUserToDTO(),
                }).ToList(),
                //WishLists = u.WishLists.Select(wl => new WishListDTO() { }),
                ReviewsList = u.ReviewList.Select(r => new ReviewDTO()
                {
                    ID = r.ID,
                    Description = r.Description,
                    Rating = r.Rating,
                }).ToList(),
                CommentsList = u.CommentList.Select(c => new CommentDTO()
                {
                    ID = c.ID,
                    LikesCount = c.LikesCount,
                }).ToList(),
                //FlagList
                //LikesList
            };
            return model;
        }
        public static  User MapToUser(this UserDTO model)
        {
            var theUser = new User()
            {
                Name = model.Name,
                Password = model.Password,
                CreatedOn = DateTime.UtcNow,
                DrankLists = new List<DrankList>(),
                WishLists = new List<WishList>(),
                ReviewList = new List<Review>(),
                CommentList = new List<Comment>(),
                FlagList = new List<Flag>(),
                LikesList = new List<Like>(),
            };
            return theUser;
        }

    }
}

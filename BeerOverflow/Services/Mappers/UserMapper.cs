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
                DrankLists = u.DrankLists.Count > 0 ?
                    u.DrankLists.Select(dl => new DrankListDTO()
                    { 
                    BeerID = dl.BeerID,
                    Beer = dl.Beer.MapBeerToDTO(),
                    UserID = dl.UserID,
                    User = dl.User.MapUserToDTO(),
                }).ToList() : null,

                WishLists = u.WishLists.Count > 0 ?
                    u.WishLists.Select(x => new WishListDTO()
                    {
                        BeerID = x.BeerID,
                        Beer = x.Beer.MapBeerToDTO(),
                        UserID = x.UserID,
                        User = x.User.MapUserToDTO(),
                    }).ToList() : null,
                ReviewsList = u.ReviewList.Count > 0 ?
                    u.ReviewList.Select(r => r.MapReviewToDTO()
                    //new ReviewDTO()
                    //{
                    //    ID = r.ID,
                    //    Description = r.Description,
                    //    Rating = r.Rating,
                    //}
                    ).ToList() : null,
                CommentsList = u.CommentList.Count > 0 ?
                    u.CommentList.Select(c => c.MapCommentToDTO()
                    //new CommentDTO()
                    //{
                    //    ID = c.ID,
                    //    LikesCount = c.LikesCount,
                    //}
                    ).ToList() : null,
                //FlagList
                //LikesList = u.LikesList.Count >0 ? u.LikesList(l => )
            };
            return model;
        }
        public static User MapToUser(this UserDTO model)
        {
            var theUser = new User()
            {
                Name = model.Name,
                Password = model.Password,
                CreatedOn = DateTime.UtcNow,
                //DrankList = new List<Beer>(),
                //WishList = model.WishList.ToHashSet() ?? new HashSet<Beer>(),
                ReviewList = new List<Review>(),
                CommentList = new List<Comment>(),
                FlagList = new List<Flag>(),
                LikesList = new List<Like>(),
            };
            return theUser;
        }

    }
}

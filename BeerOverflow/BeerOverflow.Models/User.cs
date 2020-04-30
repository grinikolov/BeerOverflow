using System;
using System.Collections.Generic;

namespace BeerOverflow.Models
{
    public class User
    {
        public int? ID { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
        public ICollection<DrankList> DrankLists { get; set; }
        public ICollection<WishList> WishLists { get; set; }
        public ICollection<Review> ReviewList { get; set; }
        public ICollection<Comment> CommentList { get; set; }
        public ICollection<Flag> FlagList { get; set; }
        public ICollection<Like> LikesList { get; set; }
        public bool IsDeleted { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }

    }
}
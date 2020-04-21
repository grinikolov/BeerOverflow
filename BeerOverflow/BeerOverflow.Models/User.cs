using System;
using System.Collections.Generic;

namespace BeerOverflow.Models
{
    public class User
    {
        public User()
        {
            this.DrankList = new HashSet<Beer>();
            this.WishList = new HashSet<Beer>();
            this.ReviewList = new List<Review>();
            this.CommentList = new List<Comment>();
            this.FlagList = new List<Flag>();
            this.LikesList = new List<Like>();
        }
        public int ID { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
        // TODO: Why not DrankList to be List<Beer> ? e.g.:
        public ICollection<Beer> DrankList { get; set; }
        public ICollection<Beer> WishList { get; set; }
       
        public ICollection<Review> ReviewList { get; set; }
        public ICollection<Comment> CommentList { get; set; }
        public ICollection<Flag> FlagList { get; set; }
        public ICollection<Like> LikesList { get; set; }
        public bool IsDeleted { get; set; }

    }
}
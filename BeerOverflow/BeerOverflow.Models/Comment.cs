using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BeerOverflow.Models
{
    public class Comment
    {
        public int? ID { get; set; }
        public int BeerID { get; set; }
        public Beer Beer { get; set; }
        public int UserID { get; set; }
        public User User { get; set; }
        public int ReviewID { get; set; }
        public Review Review { get; set; }
        public string Description { get; set; }
        public int LikesCount { get; set; }
        public ICollection<Like> Likes {get; set;}
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
        public bool IsDeleted { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace BeerOverflowAPI.Models
{
    public class ReviewViewModel
    {

        public int? ID { get; set; }
        public int? BeerID { get; set; }
        public string BeerName { get; set; }
        public int UserID { get; set; }
        public string UserName { get; set; }
        public int Rating { get; set; }
        public string Description { get; set; }
        public int LikesCount { get; set; }
        public ICollection<CommentViewModel> Comments { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsFlagged { get; set; }

    }
}

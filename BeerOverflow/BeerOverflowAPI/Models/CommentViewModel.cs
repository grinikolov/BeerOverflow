using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeerOverflowAPI.Models
{
    public class CommentViewModel
    {
        public int? ID { get; set; }
        public int BeerID { get; set; }
        public int UserID { get; set; }
        public int ReviewID { get; set; }
        public string Description { get; set; }
        public int LikesCount { get; set; }
        public ICollection<LikeViewModel> Likes { get; set; }
    }
}

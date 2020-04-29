using System;
using System.Collections.Generic;
using System.Text;

namespace Services.DTOs
{
    public class ReviewDTO
    {

        public int ID { get; set; }
        public int? BeerID { get; set; }
        public BeerDTO Beer { get; set; }
        public int UserID { get; set; }
        public UserDTO User { get; set; }
        public int Rating { get; set; }
        public string Description { get; set; }
        public int LikesCount { get; set; }
        public ICollection<CommentDTO> Comments { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsFlagged { get; set; }

    }
}

using System.Collections.Generic;

namespace Services.DTOs
{
    public class CommentDTO
    {
        public int ID { get; set; }
        public int BeerID { get; set; }
        public BeerDTO Beer { get; set; }
        public int UserID { get; set; }
        public UserDTO User { get; set; }
        public ReviewDTO Review { get; set; }
        public string Description { get; set; }
        public int LikesCount { get; set; }
        public ICollection<LikeDTO> Likes { get; set; }
    }
}
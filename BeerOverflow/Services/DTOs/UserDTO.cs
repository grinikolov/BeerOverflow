using System.Collections.Generic;

namespace Services.DTOs
{
    public class UserDTO
    {
        public int? ID { get; set; }
        public string Name { get; set; }

        public string Password { get; set; }

        public ICollection<BeerDTO> DrankList { get; set; }
        public ICollection<BeerDTO> WishList { get; set; }
        public ICollection<ReviewDTO> ReviewsList { get; set; }
        public ICollection<CommentDTO> CommentsList { get; set; }
        public ICollection<FlagDTO> FlagList { get; set; }
        public ICollection<LikeDTO> LikesList { get; set; }

    }
}
namespace Services.DTOs
{
    public class LikeDTO
    {
        public int CommentID { get; set; }
        public CommentDTO Comment { get; set; }
        public int UserID { get; set; }
        public UserDTO User { get; set; }
    }
}
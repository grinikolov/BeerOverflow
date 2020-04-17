namespace Services.DTOs
{
    public class FlagDTO
    {
        public int UserID { get; set; }
        public UserDTO User { get; set; }
        public int ReviewID { get; set; }
        public ReviewDTO Review { get; set; }
    }
}
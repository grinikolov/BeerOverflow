namespace Services.DTOs
{
    public class DrankListDTO
    {
        public int UserID { get; set; }
        public UserDTO User { get; set; }
        public int BeerID { get; set; }
        public BeerDTO Beer { get; set; }
    }
}
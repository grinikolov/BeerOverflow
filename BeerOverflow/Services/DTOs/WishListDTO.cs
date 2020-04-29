namespace Services.DTOs
{
    public class WishListDTO
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public int? BeerID { get; set; }
        public string BeerName { get; set; }
    }
}
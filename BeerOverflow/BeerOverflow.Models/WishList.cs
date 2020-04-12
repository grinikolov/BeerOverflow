using System;

namespace BeerOverflow.Models
{
    public class WishList
    {
        public int UserID { get; set; }
        public User User { get; set; }
        public int BeerID { get; set; }
        public Beer Beer { get; set; }
    }
}
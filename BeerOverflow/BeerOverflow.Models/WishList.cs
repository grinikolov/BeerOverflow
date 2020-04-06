using System;

namespace BeerOverflow.Models
{
    public class WishList
    {
        public Guid ID { get; set; }
        public User User { get; set; }
        public Guid UserID { get; set; }
        public Beer Beer { get; set; }
        public Guid BeerID { get; set; }


    }
}
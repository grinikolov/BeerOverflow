using System;

namespace BeerOverflow.Models
{
    public class DrankList
    {
        public Guid ID { get; set; }
        public User User { get; set; }
        public Guid UserID { get; set; }
        public Beer Beer { get; set; }
        public Guid BeerID { get; set; }


    }
}